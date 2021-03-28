DECLARE @ProductCount INT;

SELECT @ProductCount = COUNT(*) FROM Products

IF @ProductCount <> 0
	Return;

INSERT INTO Products (Name, PriceExcVat, PriceIncVat)

VALUES
('Advocate for Cats', 10, 12),
('Advocate for Dogs', 20, 24),
('Advantix Flea Treatment', 30, 36),
('Betafuse', 5, 6),
('Bisolvon',8, 9.60),
('Bravecto Plus', 4, 4.80),
('Bravecto Spot-On', 2, 2.40),
('Carprox',1 , 1.20),
('Equizol', 2.50, 3.00),
('Frontline Combo', 4.50, 5.40),
('Galastop',3, 3.60),
('Hypercard', 12, 14.40),
('Loxicom', 16, 19.20),
('Metacam', 5.50, 6.60),
('Milbemax',0.80, 0.96),
('Milprazon', 5, 6),
('Milpro', 4.40, 5.28),
('Nexgard', 6.80, 8.16),
('Prednicare', 9, 10.80),
('Simparaca', 5, 6)