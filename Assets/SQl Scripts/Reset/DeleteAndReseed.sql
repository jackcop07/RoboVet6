DELETE Clients
DELETE Animals
DELETE Species
DELETE Breeds
DELETE Colours
DELETE Products
DELETE Consultations
DELETE ConsultationDetails
DELETE Diaries
DELETE Appointments

DBCC CHECKIDENT ('Clients', RESEED, 0)
DBCC CHECKIDENT ('Animals', RESEED, 0)
DBCC CHECKIDENT ('Species', RESEED, 0)
DBCC CHECKIDENT ('Breeds', RESEED, 0)
DBCC CHECKIDENT ('Colours', RESEED, 0)
DBCC CHECKIDENT ('Products', RESEED, 0)
DBCC CHECKIDENT ('Consultations', RESEED, 0)
DBCC CHECKIDENT ('ConsultationDetails', RESEED, 0)
DBCC CHECKIDENT ('Diaries', RESEED, 0)
DBCC CHECKIDENT('Appointments', RESEED, 0)