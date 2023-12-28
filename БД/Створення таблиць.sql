create table Klient
(
    ID_Klient int identity(1,1) not null primary key,
    last_Name nvarchar(20) check (last_Name like N'[А-Яа-я]%' and last_Name not like N'%[0-9]%'),
    first_Name nvarchar(20) check (first_Name like N'[А-Яа-я]%' and first_Name not like N'%[0-9]%'),
    surname nvarchar(20) check (surname like N'[А-Яа-я]%' and surname not like N'%[0-9]%'),
    passport_Number char(9) check (passport_Number like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    phone_Number char(10) check (phone_Number like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    driver_License_Number nvarchar(10) check (driver_License_Number like '[A-ZА-Я][A-ZА-Я][A-ZА-Я] [0-9][0-9][0-9][0-9][0-9][0-9]'),
	password_user varchar(50) not null,
    registration_Date date
);


create table Avto
(
    ID_Avto int identity(1,1) not null primary key,
    car_Number nvarchar(20) check (car_Number like '[А-Я][А-Я] [0-9][0-9][0-9][0-9] [А-Я][А-Я]'),
    car_Brand nvarchar(20) check (car_Brand like '%[A-Za-zА-Я]%'),
    car_Model nvarchar(20) check (car_Model like '[A-Za-z]%'),
    car_Color nvarchar(20) check (car_Color like '[А-Яа-я]%' and car_Color not like N'%[0-9]%'),
    car_Year int check (car_Year like '[0-9][0-9][0-9][0-9]'),
	average_Fuel_Consumption decimal(10, 2),
	PhotoData VARBINARY(MAX),
    cost_1_3_Day_Rental decimal(10, 2),
	cost_4_9_Day_Rental decimal(10, 2),
	cost_10_25_Day_Rental decimal(10, 2),
	cost_26_Day_Rental decimal(10, 2)
);


CREATE TABLE Rent
(
    ID_Renta INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    rental_Start_Date DATE,
    end_Of_Rental DATE,
    actual_End_Of_Rental DATETIME, 
	rental_Price decimal(10, 2),

    ID_Klient INT NOT NULL FOREIGN KEY (ID_Klient) REFERENCES 
    Klient(ID_Klient) ON DELETE CASCADE ON UPDATE NO ACTION,
    
    ID_Avto INT NOT NULL FOREIGN KEY (ID_Avto) REFERENCES 
    Avto(ID_Avto) ON DELETE CASCADE ON UPDATE NO ACTION
);


create table Fines
(
    ID_Fines int identity(1,1) not null primary key,
    name_Fines nvarchar(20) check (name_Fines like N'[А-Яа-я]%'),
    sum_Fines decimal(10, 2),

    ID_Klient int not null foreign key (ID_Klient) references 
    Klient(ID_Klient) on delete cascade on update no action
);


create table Discount
(
    ID_Discount int identity(1,1) not null primary key,
    name_Discount nvarchar(20) check (name_Discount like N'[А-Яа-я]%'),
    percent_Discount INT CHECK (percent_Discount BETWEEN 1 AND 100),

    ID_Klient int not null foreign key (ID_Klient) references 
    Klient(ID_Klient) on delete cascade on update no action
);


CREATE PROCEDURE SearchInfoInKlient
    @searchText NVARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)
    SET @sql = 'SELECT * FROM Klient WHERE '

    SET @sql = @sql + 'last_Name LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'first_Name LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'surname LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'passport_Number LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'phone_Number LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'driver_License_Number LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'password_user LIKE ''%' + @searchText + '%'''

    EXEC sp_executesql @sql
END

CREATE PROCEDURE SearchInfoInAvto
    @searchText NVARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)
    SET @sql = 'SELECT * FROM Avto WHERE '

    SET @sql = @sql + 'car_Number LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'car_Brand LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'car_Model LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'car_Color LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(car_Year AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(average_Fuel_Consumption AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(cost_1_3_Day_Rental AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(cost_4_9_Day_Rental AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(cost_10_25_Day_Rental AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CAST(cost_26_Day_Rental AS NVARCHAR(MAX)) LIKE ''%' + @searchText + '%'''

    EXEC sp_executesql @sql
END


CREATE PROCEDURE SearchInfoInRent
    @searchText NVARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX)
    SET @sql = 'SELECT * FROM Rent WHERE '

    SET @sql = @sql + 'CONVERT(NVARCHAR, rental_Start_Date, 103) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CONVERT(NVARCHAR, end_Of_Rental, 103) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'CONVERT(NVARCHAR, actual_End_Of_Rental, 103) LIKE ''%' + @searchText + '%'' OR '
    SET @sql = @sql + 'ID_Klient IN (SELECT ID_Klient FROM Klient WHERE last_Name LIKE ''%' + @searchText + '%'') OR '
    SET @sql = @sql + 'ID_Avto IN (SELECT ID_Avto FROM Avto WHERE car_Number LIKE ''%' + @searchText + '%'')'

    EXEC sp_executesql @sql
END

CREATE TRIGGER trg_overdue_rent_fine
ON Rent
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ID_Renta INT;

    SELECT @ID_Renta = ID_Renta
    FROM inserted;

    IF EXISTS (
            SELECT 1
            FROM inserted
            WHERE actual_End_Of_Rental > end_Of_Rental
                AND CAST(actual_End_Of_Rental AS TIME) > '13:00'
        )
    BEGIN
        INSERT INTO Fines (name_Fines, sum_Fines, ID_Klient)
        VALUES (N'Прострочена оренда', 50.00, (SELECT ID_Klient FROM Rent WHERE ID_Renta = @ID_Renta));
    END
END;