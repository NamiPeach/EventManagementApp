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
('Olena Kovalenko', 'olena@example.com', 'hashed_password_1'),
('Maksym Ivanov', 'maksym@example.com', 'hashed_password_2');

INSERT INTO Events (Name, Date, Location) VALUES
('IT Conference', '2026-10-15', 'Kyiv'),
('Music Festival', '2026-11-02', 'Lviv'),
('Art Exhibition', '2026-09-20', 'Odesa');

INSERT INTO Registrations (OrganizerID, EventID, ParticipantName, Status) VALUES
(1, 1, 'Andrii Petrenko', 'Confirmed'),
(1, 2, 'Maria Sydorenko', 'Pending'),
(2, 3, 'Ihor Melnyk', 'Confirmed');
