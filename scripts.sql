use straightening_and_painting_workshop_b36630;

create table role_user(	role_id int not null Identity(1, 1) constraint PK_role_user Primary Key,
						role_description varchar(255) not null);

create table app_user(	email varchar(255) not null constraint PK_user Primary Key,
						pass varchar(255) not null,
						complete_name varchar(255) not null,
						role_id int not null constraint FK_app_user_role_user Foreign Key(role_id) references role_user(role_id));

create table client(	client_identification varchar(255) not null constraint PK_client Primary Key,
						name varchar(255) not null,
						lastname varchar(255) not null,
						telephone varchar(255) not null,
						address varchar(255) not null);

create table vehicle(license_number varchar(255) not null constraint PK_vehicle Primary Key,
						color varchar(255) not null,
						brand varchar(255) not null,
						style varchar(255) not null,
						year int not null,
						capacity int not null,
						weight float not null,
						chassis_number varchar(255) not null);

create table work_order(work_order_id int not null Identity(1, 1) constraint PK_work_order Primary Key,
						description varchar(255) not null,
						tentative_date date not null,
						details_price float not null,
						labor_price float not null,
						client_identification varchar(255) not null,
						license_number varchar(255) not null,
						constraint FK_work_order_client Foreign Key(client_identification) references client(client_identification) on delete cascade on update cascade,
						constraint FK_work_order_vehicle Foreign Key(license_number) references vehicle(license_number) on delete cascade on update cascade);

create table work_detail(work_detail_id int not null Identity(1, 1) constraint PK_work_detail Primary Key,
						products_price float not null,
						description varchar(255) not null,
						work_order_id int not null,
						constraint FK_work_detail_work_order Foreign Key(work_order_id) references work_order(work_order_id) on delete cascade on update cascade);

create table required_product(required_product_id int not null Identity(1, 1) constraint PK_required_product Primary Key,
							material varchar(255) not null,
							quantity int not null,
							price float not null,
							work_detail_id int not null,
							constraint FK_required_product_work_detail Foreign Key(work_detail_id) references work_detail(work_detail_id) on delete cascade on update cascade);



Create PROCEDURE insert_app_user @email varchar(255), @pass varchar(255), @complete_name varchar(255), @role_id int
AS
BEGIN
	insert into app_user(email, pass, complete_name, role_id) values(@email, @pass, @complete_name, @role_id);
END


Create PROCEDURE insert_client @client_identification varchar(255), @name varchar(255), @lastname varchar(255), @telephone varchar(255), @address varchar(255)
AS
BEGIN
	insert into client(client_identification, name, lastname, telephone, address)
	values(@client_identification, @name, @lastname, @telephone, @address);
END

Create PROCEDURE update_client @client_identification varchar(255), @name varchar(255), @lastname varchar(255), @telephone varchar(255), @address varchar(255)
AS
BEGIN
	update client set name=@name, lastname=@lastname, telephone=@telephone, address=@address where client_identification=@client_identification;
END

Create PROCEDURE insert_vehicle @license_number varchar(255), @color varchar(255), @brand varchar(255), @style varchar(255), @year int, @capacity int, @weight float, @chassis_number varchar(255)
AS
BEGIN
	insert into vehicle(license_number, color, brand, style, year, capacity, weight, chassis_number)
	values(@license_number, @color, @brand, @style, @year, @capacity, @weight, @chassis_number);
END


Create PROCEDURE update_vehicle @license_number varchar(255), @color varchar(255), @brand varchar(255), @style varchar(255), @year int, @capacity int, @weight float, @chassis_number varchar(255)
AS
BEGIN
	update vehicle set color=@color, brand=@brand, style=@style, year=@year, capacity=@capacity, weight=@weight, chassis_number=@chassis_number where license_number=@license_number;
END

Create PROCEDURE insert_work_order @description varchar(255), @tentative_date date, @client_identification varchar(255), @license_number varchar(255)
AS
BEGIN
	insert into work_order(description, tentative_date, details_price, labor_price, client_identification, license_number)
	values(@description, @tentative_date, 0, 0, @client_identification, @license_number);

	SELECT @@IDENTITY AS 'work_order_id';
END

Create PROCEDURE update_work_order @work_order_id int, @description varchar(255), @tentative_date date, @details_price float, @labor_price float, @client_identification varchar(255), @license_number varchar(255)
AS
BEGIN
	update work_order set description=@description, tentative_date=@tentative_date, details_price=@details_price, labor_price=@labor_price, client_identification=@client_identification, license_number=@license_number where work_order_id = @work_order_id;
END

Create PROCEDURE insert_work_detail @description varchar(255), @work_order_id int
AS
BEGIN
	insert into work_detail(products_price, description, work_order_id) values(0, @description, @work_order_id);

	SELECT @@IDENTITY AS 'work_detail_id';
END

Create PROCEDURE update_work_detail @work_detail_id int, @products_price float, @description varchar(255), @work_order_id int
AS
BEGIN
	update work_detail set products_price=@products_price, description=@description, work_order_id=@work_order_id where work_detail_id = @work_detail_id;
END

---

Create PROCEDURE update_required_product @required_product_id int, @material varchar(255), @quantity int, @price float, @work_detail_id int
AS
BEGIN
	update required_product set material=@material, quantity=@quantity, price=@price, work_detail_id=@work_detail_id where required_product_id = @required_product_id;
END









Create PROCEDURE insert_required_product @material varchar(255), @quantity int, @price float, @work_detail_id int
AS
BEGIN
	Declare @productPrice float;
	Declare @work_order_id int;

	Begin Transaction transactionForPrices
		BEGIN TRY
			insert into required_product(material, quantity, price, work_detail_id) values(@material, @quantity, @price, @work_detail_id);
			SET @productPrice = @quantity*@price;

			update work_detail set products_price = products_price + @productPrice where work_detail_id = @work_detail_id;

			select @work_order_id = work_order_id from work_detail where work_detail_id = @work_detail_id;

			update work_order set details_price = details_price + @productPrice where work_order_id = @work_order_id;

		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRANSACTION transactionForPrices;
				END
		END CATCH
    
	COMMIT TRANSACTION
END


--insert into required_product(material, quantity, price, work_detail_id) values('tela', 2, 3000, 1);

exec insert_required_product 'tela', 2, 3000, 1;