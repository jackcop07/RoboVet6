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

	INSERT INTO Breeds (Name, SpeciesId)
	VALUES
	('Labrador Retriever', '1'),
	('German Shepherd', '1'),
	('Golden Retriever', '1'),
	('Bulldog', '1'),
	('Beagle', '1'),
	('Poodle', '1'),
	('Siamese', '2'),
	('Persian', '2'),
	('Maine Coon', '2'),
	('Ragdoll', '2'),
	('Bengal', '2'),
	('Abyssinian', '2'),
	('Budgie', '3'),
	('Cockatiel', '3'),
	('Finch', '3'),
	('Scorpion', '4'),
	('Llama', '4'),
	('Tarantula', '4'),
	('Russian Tortoise', '5'),
	('Bearded Dragon', '5'),
	('Crested Gecko', '5'),
	('Praying Mantis', '6'),
	('Stick Insects', '6')


IF(@ClientCount = 0)
BEGIN
	INSERT INTO Colours (Name)
	VALUES
	('Black'),
	('Brown'),
	('Red'),
	('Ruby'),
	('Yellow'),
	('Gold'),
	('Grey'),
	('Blue'),
	('Sable'),
	('White'),
	('Albino'),
	('Buff')
END


DECLARE @AnimalCount INT
SELECT @AnimalCount = COUNT(*) FROM Animals
IF (@AnimalCount = 0)
BEGIN
	INSERT INTO Animals(Name, ClientId, SpeciesId, BreedId)
	VALUES
	('Toby', '1', '1', '1'),
	('Seb', '1', '3', '15'),
	('Max', '1', '2', '7'),
	('Paddy', '2', '4', '16'),
	('Patch', '2', '5', '19'),
	('Richard', '2', '6', '22'),
	('Lucky', '3', '2', '7'),
	('Timmy', '3', '1', '1'),
	('Sam', '4', '2', '7'),
	('Jimbo', '4', '1', '1'),
	('Sascha', '4', '1', '1'),
	('Rox', '4', '4', '16'),
	('Lucy', '5', '4', '16'),
	('Polly', '5', '5', '19'),
	('Ruby', '5', '6', '22'),
	('Sooty', '5', '2', '7')
END