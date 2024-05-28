CREATE TABLE Country(
	Id serial PRIMARY KEY,
	countryName character varying(20)
);

INSERT INTO Country (countryName) VALUES
('United States'),
('Canada'),
('India');

SELECT * FROM Country
	
CREATE TABLE City(
	Id serial PRIMARY KEY,
	Country_Id integer NOT NULL REFERENCES	Country(Id),
	cityName character varying(20)
);

INSERT INTO City (Country_Id, cityName) VALUES
(1, 'New York'),
(1, 'Los Angeles'),
(2, 'Toronto'),
(3, 'Mumbai');

SELECT * FROM City

CREATE TABLE "User"(
	Id serial primary key,
	FirstName character varying not null,
	LastName character varying not null,
	PhoneNumber character varying(10),
	EmailAddress character varying not null unique,
	UserType character varying,
	"Password" text
);

INSERT INTO "User" (FirstName, LastName, PhoneNumber, EmailAddress, UserType, "Password") VALUES
('Ruchit', 'Ukani', '8799498443', 'ruchitukani1501@gmail.com', 'Volunteer', 'password123'),
('Vikas', 'Adodariy', '0987654321', 'vikas.adoda@example.com', 'Volunteer', 'password456'),
('Shyam', 'Panchotiya', '5555555555', 'shyam.pancho@example.com', 'Volunteer', 'password789');

SELECT * FROM "User"

CREATE TABLE UserDetails(
	Id serial primary key,
	User_Id integer references "User"(Id),
	"Name" character varying,
	Surname character varying,
	EmployeeId character varying,
	Manager character varying,
	Title character varying,
	Department character varying,
	MyProfile text,
	WhyIVolunteer text,
	Country_Id integer references Country(Id),
	City_Id integer references City(Id),
	Availability text,
	LinkedInUrl text,
	MySkills text,
	UserImage text,
	Status boolean
);

INSERT INTO UserDetails (User_Id, "Name", Surname, EmployeeId, Manager, Title, Department, MyProfile, WhyIVolunteer, Country_Id, City_Id, Availability, LinkedInUrl, MySkills, UserImage, Status) VALUES
(1, 'Ruchit', 'Ukani', 'E123', 'Manager1', 'Engineer', 'IT', 'Profile of Ruchit', 'I want to help.', 1, 1, 'Weekends', 'https://linkedin.com/in/ruchitukani', 'Engineering, Programming', 'image1.jpg', true),
(2, 'Vikas', 'Adodariya', 'E124', 'Manager2', 'Designer', 'Design', 'Profile of Vikas', 'I love volunteering.', 2, 3, 'Weekdays', 'https://linkedin.com/in/vikasadodariya', 'Design, Art', 'image2.jpg', true),
(3, 'Shyam', 'Panchotiya', 'E125', 'Manager3', 'Analyst', 'Finance', 'Profile of Shyam', 'Giving back is important.', 3, 4, 'Evenings', 'https://linkedin.com/in/shyampanchotiya', 'Analysis, Finance', 'image3.jpg', true);

select * from UserDetails

CREATE TABLE UserSkills(
	Id serial primary key,
	Skills character varying not null,
	User_Id integer references "User"(Id)
);

INSERT INTO UserSkills (Skills, User_Id) VALUES
('Programming', 1),
('Design', 2),
('Finance', 3);

select * from UserSkills

CREATE TABLE MissionSkill(
	Id serial primary key,
	SkillName character varying(255) not null,
	Status character varying
);

INSERT INTO MissionSkill (SkillName, Status) VALUES
('Programming', 'Active'),
('Design', 'Active'),
('Finance', 'Active');

select * from MissionSkill

CREATE TABLE MissionTheme(
	Id serial primary key,
	ThemeName character varying(255) not null,
	Status character varying
);

INSERT INTO MissionTheme (ThemeName, Status) VALUES
('Education', 'Active'),
('Health', 'Active'),
('Environment', 'Active');

select * from MissionTheme

CREATE TABLE Missions (
    Id serial primary key,
	MissionTitle character varying not null,
	MissionDescription text,
	MissionOrganizationName character varying,
	MissionOrganizationDetails text,
	Country_Id integer references Country(Id),
	City_Id integer references City(Id),
	StartDate date not null,
	EndDate date not null,
	MissionType character varying(200),
	TotalSheets integer,
	RegistrationDeadLine date,
	MissionTheme_Id integer references MissionTheme(Id),
	MissionSkill_Id integer references MissionSkill(Id),
	MissionImage text,
	MissionDocuments text,
	MissionAvilability text,
	MissionVideoUrl text
);

INSERT INTO Missions (MissionTitle, MissionDescription, MissionOrganizationName, MissionOrganizationDetails, Country_Id, City_Id, StartDate, EndDate, MissionType, TotalSheets, RegistrationDeadLine, MissionTheme_Id, MissionSkill_Id, MissionImage, MissionDocuments, MissionAvilability, MissionVideoUrl) VALUES
('Teach Programming', 'Teach programming to underprivileged kids.', 'Tech for Good', 'A non-profit organization.', 1, 1, '2024-06-01', '2024-12-01', 'Long-term', 50, '2024-05-31', 1, 1, 'mission1.jpg', 'doc1.pdf', 'Weekends', 'video1.mp4'),
('Design Workshop', 'Conduct a design workshop for high school students.', 'Creative Minds', 'A non-profit organization.', 2, 3, '2024-06-01', '2024-12-01', 'Long-term', 50, '2024-05-31', 2, 2, 'mission2.jpg', 'doc2.pdf', 'Weekdays', 'video2.mp4'),
('Finance Literacy', 'Provide financial literacy education to adults.', 'Finance Forward', 'A non-profit organization.', 3, 4, '2024-06-01', '2024-12-01', 'Long-term', 50, '2024-05-31', 3, 3, 'mission3.jpg', 'doc3.pdf', 'Evenings', 'video3.mp4');

select * from Missions

CREATE TABLE MissionApplication (
    Id serial primary key,
    Mission_Id int references Missions(Id),
    User_Id int references "User"(Id),
    AppliedDate timestamp not null default now(),
    Status boolean,
    Sheet integer
);

INSERT INTO MissionApplication (Mission_Id, User_Id, AppliedDate, Status, Sheet) VALUES
(1, 1, '2024-05-20 10:00:00', true, 1),
(2, 2, '2024-05-21 11:00:00', true, 1),
(3, 3, '2024-05-22 12:00:00', true, 1);

select * from MissionApplication

