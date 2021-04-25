DECLARE @DiaryCount INT;

SELECT @DiaryCount = COUNT(*) FROM Diaries

IF @DiaryCount <> 0
	Return;

INSERT INTO Diaries(Name)

VALUES
('Consultation 1'),
('Consultation 2'),
('Hydrotherapy'),
('Nurse Room')