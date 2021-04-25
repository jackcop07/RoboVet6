DECLARE @ApptCount INT;

SELECT @ApptCount = COUNT(*) FROM Appointments

IF @ApptCount <> 0
	Return;


INSERT INTO Appointments (AnimalId, DiaryId, StartTime, AppointmentLength, Notes)
VALUES
('1', '1', '2021-04-16 10:00:00', '30', 'Very poorly puppy.'),
('1', '2', '2021-04-16 11:00:00', '30', 'Wormer.'),
('2', '2', '2021-04-16 12:00:00', '30', 'Yearly vaccinations.'),
('2', '3', '2021-04-16 13:00:00', '30', 'Dental check-up.'),
('3', '4', '2021-04-16 14:00:00', '30', 'Ate something she shouldnt have..'),
('3', '5', '2021-04-17 15:00:00', '30', 'Flea and wormer.'),
('4', '1', '2021-04-17 10:00:00', '30', 'Yearly check-up.'),
('4', '2', '2021-04-17 11:00:00', '30', 'Sore paw.'),
('1', '1', '2021-04-17 12:00:00', '30', 'Nail clipping.'),
('1', '2', '2021-04-17 13:00:00', '30', 'X-ray.')