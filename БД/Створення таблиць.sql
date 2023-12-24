create table Klient
(
    ID_Klient int identity(1,1) not null primary key,
    last_Name nvarchar(20) check (last_Name like N'[Р-пр-џ]%' and last_Name not like N'%[0-9]%'),
    first_Name nvarchar(20) check (first_Name like N'[Р-пр-џ]%' and first_Name not like N'%[0-9]%'),
    surname nvarchar(20) check (surname like N'[Р-пр-џ]%' and surname not like N'%[0-9]%'),
    passport_Number char(9) check (passport_Number like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    phone_Number char(10) check (phone_Number like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    driver_License_Number nvarchar(10) check (driver_License_Number like '[A-ZР-п][A-ZР-п][A-ZР-п] [0-9][0-9][0-9][0-9][0-9][0-9]'),
	password_user varchar(50) not null,
    registration_Date date
);


create table Avto
(
    ID_Avto int identity(1,1) not null primary key,
    car_Number nvarchar(20) check (car_Number like '[Р-п][Р-п] [0-9][0-9][0-9][0-9] [Р-п][Р-п]'),
    car_Brand nvarchar(20) check (car_Brand like '%[A-Za-zР-п]%'),
    car_Model nvarchar(20) check (car_Model like '[A-Za-z]%'),
    car_Color nvarchar(20) check (car_Color like '[Р-пр-џ]%' and car_Color not like N'%[0-9]%'),
    car_Year int check (car_Year like '[0-9][0-9][0-9][0-9]'),
	average_Fuel_Consumption decimal(10, 2),
	PhotoData VARBINARY(MAX),
    cost_1_3_Day_Rental decimal(10, 2),
	cost_4_9_Day_Rental decimal(10, 2),
	cost_10_25_Day_Rental decimal(10, 2),
	cost_26_Day_Rental decimal(10, 2)
);