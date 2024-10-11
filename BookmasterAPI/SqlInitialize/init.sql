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
    "AdminId" INT NOT NULL,
    "DateFrom" DATE NOT NULL,
    "DateTo" DATE,
    "ReturnDate" DATE,
    FOREIGN KEY ("ClientId") REFERENCES "Clients"("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id"),
    FOREIGN KEY ("AdminId") REFERENCES "Administrators"("Id")
);

INSERT INTO "Covers" ("Description") 
VALUES ('Пример обложки'); 

INSERT INTO "Books" ("Title", "Subtitle", "Description", "PublicationDate", "CoverId") 
VALUES 
('Harry Potter', 'A young wizard discovering his powers', 'A young wizard discovering his powers', '1997-06-26', NULL),
('The Hobbit', 'A hobbit goes on an unexpected journey', 'A hobbit goes on an unexpected journey', '1937-09-21', NULL),
('1984', 'A dystopian future novel', 'A dystopian future novel', '1949-06-08', NULL);

INSERT INTO "Clients" ("Name", "City", "Zip", "Address", "Email", "Phone") 
VALUES 
('Иван Иванов', 'Москва', '101000', 'ул. Пушкина, д. 1', 'ivan.ivanov@example.com', '1234567890'),
('Анна Смирнова', 'Санкт-Петербург', '190000', 'ул. Ленина, д. 2', 'anna.smirnova@example.com', '0987654321'),
('Петр Петров', 'Екатеринбург', '620000', 'ул. Чехова, д. 3', 'petr.petrov@example.com', '1357924680');

INSERT INTO "Authors" ("Name")
VALUES 
('J.K. Rowling'),
('J.R.R. Tolkien'),
('George Orwell');

INSERT INTO "Subjects" ("Name")
VALUES 
('Fantasy'),
('Adventure'),
('Dystopia');

INSERT INTO "BookAuthors" ("BookId", "AuthorId")
VALUES 
((SELECT "Id" FROM "Books" WHERE "Title" = 'Harry Potter'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'J.K. Rowling')),
((SELECT "Id" FROM "Books" WHERE "Title" = 'The Hobbit'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'J.R.R. Tolkien')),
((SELECT "Id" FROM "Books" WHERE "Title" = '1984'), (SELECT "Id" FROM "Authors" WHERE "Name" = 'George Orwell'));

INSERT INTO "BookSubjects" ("BookId", "SubjectId")
VALUES 
((SELECT "Id" FROM "Books" WHERE "Title" = 'Harry Potter'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Fantasy')),
((SELECT "Id" FROM "Books" WHERE "Title" = 'The Hobbit'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Fantasy')),
((SELECT "Id" FROM "Books" WHERE "Title" = '1984'), (SELECT "Id" FROM "Subjects" WHERE "Name" = 'Dystopia'));

