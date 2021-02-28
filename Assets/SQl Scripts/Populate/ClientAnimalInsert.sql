TRUNCATE TABLE Clients

DECLARE @ClientCount INT
SELECT @ClientCount = COUNT(*) FROM Clients

IF(@ClientCount = 0)
BEGIN
	INSERT INTO Clients (Title, FirstName, LastName, Address, City, Postcode, HomePhone, MobilePhone, WorkPhone, Email)
	VALUES
	('Mr', 'Jack', 'Copeland', '30 Innes Neuk', 'Wallyford', 'EH21 8EW', NULL, '07895500692', NULL, 'jack.copeland7@gmail.com'),
	('Miss', 'Lauren', 'Danagher', '19/6 Wardlaw Street', 'Edinburgh', 'EH11 1TN', '0131 243536', '07856472543', '0131 7384937', 'l.danagher@gmail.com'),
	('Dr', 'Peter', 'Smith', '2 High Street', 'Glasgow', 'G1 2HY', '0141 637485', '07554436475', '0141 3647586', 'doctors@nhs.com'),
	('Mrs', 'Sarah', 'Punting', '23 Harris Avenue', 'Manchester', 'M11 2EW', NULL, '07864590098', NULL, 'puntingfamily@gmail.com'),
	('Master', 'Brian', 'Ferris', '9 Lumbard Terrace', 'Portsmouth', 'PO21 8IU', NULL, '07996637383', NULL, 'brian122@hotmail.com')
END


TRUNCATE TABLE Animals

DECLARE @AnimalCount INT
SELECT @AnimalCount = COUNT(*) FROM Animals
IF (@AnimalCount = 0)
BEGIN
	INSERT INTO Animals(Name, ClientId)
	VALUES
	('Toby', '1'),
	('Seb', '1'),
	('Max', '1'),
	('Paddy', '2'),
	('Patch', '2'),
	('Richard', '2'),
	('Lucky', '3'),
	('Timmy', '3'),
	('Sam', '4'),
	('Jimbo', '4'),
	('Sascha', '4'),
	('Rox', '4'),
	('Lucy', '5'),
	('Polly', '5'),
	('Ruby', '5'),
	('Sooty', '5')
END