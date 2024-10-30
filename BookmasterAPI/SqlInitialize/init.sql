CREATE TABLE "Authors" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "Bio" TEXT,
    "BirthDate" DATE,
    "DeathDate" DATE,
    "Wikipedia" VARCHAR(255)
);

CREATE TABLE "Subjects" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL
);

CREATE TABLE "Covers" (
    "Id" SERIAL PRIMARY KEY,
    "Description" VARCHAR(255) NOT NULL
);

CREATE TABLE "Books" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "Subtitle" VARCHAR(255),
    "Description" TEXT,
    "PublicationDate" DATE,
    "CoverId" INT,
    FOREIGN KEY ("CoverId") REFERENCES "Covers"("Id")
);

CREATE TABLE "BookAuthors" (
    "BookId" INT NOT NULL,
    "AuthorId" INT NOT NULL,
    PRIMARY KEY ("BookId", "AuthorId"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id"),
    FOREIGN KEY ("AuthorId") REFERENCES "Authors"("Id")
);

CREATE TABLE "BookSubjects" (
    "BookId" INT NOT NULL,
    "SubjectId" INT NOT NULL,
    PRIMARY KEY ("BookId", "SubjectId"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id"),
    FOREIGN KEY ("SubjectId") REFERENCES "Subjects"("Id")
);

CREATE TABLE "Clients" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL,
    "City" VARCHAR(255),
    "Zip" VARCHAR(50) NOT NULL,
    "Address" VARCHAR(255),
    "Email" VARCHAR(255),
    "Phone" VARCHAR(50)
);

CREATE TABLE "Administrators" (
    "Id" SERIAL PRIMARY KEY,
    "Login" VARCHAR(255) NOT NULL,
    "Password" VARCHAR(255) NOT NULL
);

CREATE TABLE "Issues" (
    "Id" SERIAL PRIMARY KEY,
    "ClientId" INT NOT NULL,
    "BookId" INT NOT NULL,
    "DateFrom" DATE NOT NULL,
    "DateTo" DATE,
    "ReturnDate" DATE,
    FOREIGN KEY ("ClientId") REFERENCES "Clients"("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id")
);

INSERT INTO "Covers" ("Description") 
VALUES ('Пример обложки'); 

INSERT INTO "Books" ("Title", "Subtitle", "Description", "PublicationDate", "CoverId") 
VALUES 
('Harry Potter', 'A young wizard discovering his powers', 'A young wizard discovering his powers', '1997-06-26', NULL),
('The Hobbit', 'A hobbit goes on an unexpected journey', 'A hobbit goes on an unexpected journey', '1937-09-21', NULL),
('1984', 'A dystopian future novel', 'A dystopian future novel', '1949-06-08', NULL),
('To Kill a Mockingbird', 'A novel about racial injustice', 'A novel about racial injustice in the American South', '1960-07-11', NULL),
('Pride and Prejudice', 'A romantic story of manners', 'A romantic story about the British gentry', '1813-01-28', NULL),
('The Great Gatsby', 'A tale of the American Dream', 'A story about wealth and aspiration in the 1920s', '1925-04-10', NULL),
('Moby Dick', 'The pursuit of a great white whale', 'A captain obsessive quest for revenge against a whale', '1851-10-18', NULL),
('War and Peace', 'A novel of Russian society during the Napoleonic era', 'An epic historical novel set during Napoleon invasion of Russia', '1869-01-01', NULL),
('The Catcher in the Rye', 'A teenager journey through New York City', 'A story of teenage alienation and angst', '1951-07-16', NULL),
('The Odyssey', 'The journey of Odysseus', 'The epic journey of Odysseus returning home after the Trojan War', '800-01-01 BC', NULL);

INSERT INTO "Clients" ("Name", "City", "Zip", "Address", "Email", "Phone") 
VALUES 
('Иван Иванов', 'Москва', '101000', 'ул. Пушкина, д. 1', 'ivan.ivanov@example.com', '1234567890'),
('Анна Смирнова', 'Санкт-Петербург', '190000', 'ул. Ленина, д. 2', 'anna.smirnova@example.com', '0987654321'),
('Петр Петров', 'Екатеринбург', '620000', 'ул. Чехова, д. 3', 'petr.petrov@example.com', '1357924680'),
('Ольга Кузнецова', 'Новосибирск', '630000', 'ул. Мира, д. 4', 'olga.kuznetsova@example.com', '2468135790'),
('Дмитрий Соколов', 'Казань', '420000', 'ул. Лобачевского, д. 5', 'dmitry.sokolov@example.com', '1122334455'),
('Мария Крылова', 'Нижний Новгород', '603000', 'ул. Белинского, д. 6', 'maria.krylova@example.com', '2233445566'),
('Алексей Морозов', 'Самара', '443000', 'ул. Галактионовская, д. 7', 'alexey.morozov@example.com', '3344556677'),
('Елена Попова', 'Омск', '644000', 'ул. Фрунзе, д. 8', 'elena.popova@example.com', '4455667788'),
('Сергей Новиков', 'Челябинск', '454000', 'ул. Свободы, д. 9', 'sergey.novikov@example.com', '5566778899'),
('Наталья Федорова', 'Уфа', '450000', 'ул. Коммунистическая, д. 10', 'natalia.fedorova@example.com', '6677889900');

INSERT INTO "Authors" ("Name")
VALUES 
('J.K. Rowling'),
('J.R.R. Tolkien'),
('George Orwell'),
('Harper Lee'),
('Jane Austen'),
('F. Scott Fitzgerald'),
('Herman Melville'),
('Leo Tolstoy'),
('J.D. Salinger'),
('Homer');

INSERT INTO "Subjects" ("Name")
VALUES 
('Fantasy'),
('Adventure'),
('Dystopia'),
('Historical Fiction'),
('Romance'),
('Classics'),
('Philosophy'),
('Mythology');

INSERT INTO "BookAuthors" ("BookId", "AuthorId")
VALUES 
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Harry Potter'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'J.K. Rowling')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Hobbit'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'J.R.R. Tolkien')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = '1984'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'George Orwell')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'To Kill a Mockingbird'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'Harper Lee')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Pride and Prejudice'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'Jane Austen')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Great Gatsby'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'F. Scott Fitzgerald')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Moby Dick'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'Herman Melville')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'War and Peace'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'Leo Tolstoy')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Catcher in the Rye'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'J.D. Salinger')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Odyssey'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'Homer'));

INSERT INTO "BookSubjects" ("BookId", "SubjectId")
VALUES 
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Harry Potter'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Fantasy')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Hobbit'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Fantasy')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = '1984'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Dystopia')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'To Kill a Mockingbird'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Historical Fiction')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Pride and Prejudice'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Romance')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Great Gatsby'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Classics')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'Moby Dick'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Adventure')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'War and Peace'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Philosophy')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Catcher in the Rye'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Classics')),
    ((SELECT "Id" FROM "Books" WHERE "Title" = 'The Odyssey'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Mythology'));

INSERT INTO "Issues" ("ClientId", "BookId", "DateFrom", "DateTo", "ReturnDate") 
VALUES 
(1, 1,  '2024-10-01', '2024-10-15', '2024-10-10'),
(2, 2,  '2024-10-02', '2024-10-16', NULL),
(3, 3,  '2024-10-03', '2024-10-17', '2024-10-14'),
(4, 4,  '2024-10-04', '2024-10-18', NULL),
(5, 5,  '2024-10-05', '2024-10-19', '2024-10-18'),
(6, 6,  '2024-10-06', '2024-10-20', NULL),
(7, 7,  '2024-10-07', '2024-10-21', '2024-10-20'),
(8, 8,  '2024-10-08', '2024-10-22', NULL),
(9, 9,  '2024-10-09', '2024-10-23', '2024-10-22'),
(10, 10,  '2024-10-10', '2024-10-24', NULL);