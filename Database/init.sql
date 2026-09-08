CREATE TABLE Organizers (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL
);

CREATE TABLE Events (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Date TEXT NOT NULL,
    Location TEXT NOT NULL
);

CREATE TABLE Registrations (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    OrganizerID INTEGER NOT NULL,
    EventID INTEGER NOT NULL,
    ParticipantName TEXT NOT NULL,
    Status TEXT NOT NULL,
    FOREIGN KEY (OrganizerID) REFERENCES Organizers(ID),
    FOREIGN KEY (EventID) REFERENCES Events(ID)
);

INSERT INTO Organizers (Name, Email, PasswordHash) VALUES
('Ivan Iliuk', 'ivan@gmil.com', 'hashed_password_1'),
('Maksym reseller', 'reseller@Outlook.com', 'hashed_password_2');

INSERT INTO Events (Name, Date, Location) VALUES
('pick a peach', '2026-9-15', 'Kyiv'),
('pick a plum', '2026-10-02', 'Lviv'),
('pick a nectarine', '2026-07-20', 'Odesa');

INSERT INTO Registrations (OrganizerID, EventID, ParticipantName, Status) VALUES
(1, 1, 'Andrii Petrenko', 'Confirmed'),
(1, 2, 'Maria Sydorenko', 'Pending'),
(2, 3, 'Ihor Melnyk', 'Confirmed');
