use straightening_and_painting_workshop_b36630;

create table role_user(	role_id int not null Identity(1, 1) constraint PK_role_user Primary Key,
						role_description varchar(255) not null);

create table app_user(	email varchar(255) not null constraint PK_user Primary Key,
						pass varchar(255) not null,
						complete_name varchar(255) not null,
						role_id int not null constraint FK_app_user_role_user Foreign Key(role_id) references role_user(role_id));

Create PROCEDURE insert_app_user @email varchar(255), @pass varchar(255), @complete_name varchar(255), @role_id int
AS
BEGIN
	insert into app_user(email, pass, complete_name, role_id) values(@email, @pass, @complete_name, @role_id);
END
