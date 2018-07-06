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