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

DECLARE @SpeciesCount INT
SELECT @SpeciesCount = COUNT(*) FROM Species

IF(@ClientCount = 0)
BEGIN
	INSERT INTO Species (Name)
	VALUES
	('Dog'),
	('Cat'),
	('Bird'),
	('Exotic'),
	('Reptile'),
	('Insect')
END


DECLARE @AnimalCount INT
SELECT @AnimalCount = COUNT(*) FROM Animals
IF (@AnimalCount = 0)
BEGIN
	INSERT INTO Animals(Name, ClientId, SpeciesId)
	VALUES
	('Toby', '1', '1'),
	('Seb', '1', '3'),
	('Max', '1', '2'),
	('Paddy', '2', '4'),
	('Patch', '2', '5'),
	('Richard', '2', '6'),
	('Lucky', '3', '2'),
	('Timmy', '3', '1'),
	('Sam', '4', '2'),
	('Jimbo', '4', '1'),
	('Sascha', '4', '1'),
	('Rox', '4', '4'),
	('Lucy', '5', '4'),
	('Polly', '5', '5'),
	('Ruby', '5', '6'),
	('Sooty', '5', '2')
END