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

CREATE TABLE "Exhibitions" (
    "Id" SERIAL PRIMARY KEY,
    "CreatedDate" DATE,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL
);

CREATE TABLE "Books" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "Subtitle" VARCHAR(255),
    "Description" TEXT,
    "PublicationDate" DATE
);

CREATE TABLE "ExhibitionBooks" (
    "ExhibitionId" INT NOT NULL,
    "BookId" INT NOT NULL,
    PRIMARY KEY ("ExhibitionId", "BookId"),
    FOREIGN KEY ("ExhibitionId") REFERENCES "Exhibitions" ("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books" ("Id")
);

CREATE TABLE "Covers" (
    "Id" SERIAL PRIMARY KEY,
    "Description" VARCHAR(255) NOT NULL,
    "ImageUrl" TEXT,
    "BookId" INT,
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id")
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
    "ClientCardId" INT NOT NULL,
    "ClientCardId1" INT,
    "ClientCardId2" INT,
    "BookId" INT NOT NULL,
    "IssueFrom" DATE NOT NULL,
    "IssueTo" DATE,
    "ReturnDate" DATE,
    FOREIGN KEY ("ClientCardId") REFERENCES "Clients"("Id"),
    FOREIGN KEY ("ClientCardId1") REFERENCES "Clients"("Id"),
    FOREIGN KEY ("ClientCardId2") REFERENCES "Clients"("Id"),
    FOREIGN KEY ("BookId") REFERENCES "Books"("Id")
);

CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) UNIQUE NOT NULL,
    "Password" VARCHAR(255) NOT NULL
);

INSERT INTO "Users" ("Username", "Password") VALUES
('user1', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user2', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user3', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user4', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user5', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user6', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user7', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user8', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user9', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG'),
('user10', '$2a$11$cQxgooxZqEEhTH3E0mpTt.ylaV07f3oSFGlXmJzpL5d35TQhgp1AG');


INSERT INTO "Exhibitions" ("Name", "Description")
VALUES
('Modern Art', 'An exhibition showcasing modern art pieces.'),
('Classic Literature', 'An exhibition of books from classic literature.'),
('Science Innovations', 'A display of books and articles about scientific discoveries.');


INSERT INTO "Books" ("Title", "Subtitle", "Description", "PublicationDate") 
VALUES 
('Harry Potter', 'A young wizard discovering his powers', 'A young wizard discovering his powers', '1997-06-26'),
('The Hobbit', 'A hobbit goes on an unexpected journey', 'A hobbit goes on an unexpected journey', '1937-09-21'),
('1984', 'A dystopian future novel', 'A dystopian future novel', '1949-06-08'),
('To Kill a Mockingbird', 'A novel about racial injustice', 'A novel about racial injustice in the American South', '1960-07-11'),
('Pride and Prejudice', 'A romantic story of manners', 'A romantic story about the British gentry', '1813-01-28'),
('The Great Gatsby', 'A tale of the American Dream', 'A story about wealth and aspiration in the 1920s', '1925-04-10'),
('Moby Dick', 'The pursuit of a great white whale', 'A captain obsessive quest for revenge against a whale', '1851-10-18'),
('War and Peace', 'A novel of Russian society during the Napoleonic era', 'An epic historical novel set during Napoleon invasion of Russia', '1869-01-01'),
('The Catcher in the Rye', 'A teenager journey through New York City', 'A story of teenage alienation and angst', '1951-07-16'),
('The Odyssey', 'The journey of Odysseus', 'The epic journey of Odysseus returning home after the Trojan War', '800-01-01'),
('Book 1', 'Subtitle 1', 'Description 1', '2001-01-01'),
('Book 2', 'Subtitle 2', 'Description 2', '2001-02-01'),
('Book 3', 'Subtitle 3', 'Description 3', '2001-03-01'),
('Book 4', 'Subtitle 4', 'Description 4', '2001-04-01'),
('Book 5', 'Subtitle 5', 'Description 5', '2001-05-01'),
('Book 6', 'Subtitle 6', 'Description 6', '2001-06-01'),
('Book 7', 'Subtitle 7', 'Description 7', '2001-07-01'),
('Book 8', 'Subtitle 8', 'Description 8', '2001-08-01'),
('Book 9', 'Subtitle 9', 'Description 9', '2001-09-01'),
('Book 10', 'Subtitle 10', 'Description 10', '2001-10-01'),
('Book 11', 'Subtitle 11', 'Description 11', '2001-11-01'),
('Book 12', 'Subtitle 12', 'Description 12', '2001-12-01'),
('Book 13', 'Subtitle 13', 'Description 13', '2002-01-01'),
('Book 14', 'Subtitle 14', 'Description 14', '2002-02-01'),
('Book 15', 'Subtitle 15', 'Description 15', '2002-03-01'),
('Book 16', 'Subtitle 16', 'Description 16', '2002-04-01'),
('Book 17', 'Subtitle 17', 'Description 17', '2002-05-01'),
('Book 18', 'Subtitle 18', 'Description 18', '2002-06-01'),
('Book 19', 'Subtitle 19', 'Description 19', '2002-07-01'),
('Book 20', 'Subtitle 20', 'Description 20', '2002-08-01'),
('Book 21', 'Subtitle 21', 'Description 21', '2002-09-01'),
('Book 22', 'Subtitle 22', 'Description 22', '2002-10-01'),
('Book 23', 'Subtitle 23', 'Description 23', '2002-11-01'),
('Book 24', 'Subtitle 24', 'Description 24', '2002-12-01'),
('Book 25', 'Subtitle 25', 'Description 25', '2003-01-01'),
('Book 26', 'Subtitle 26', 'Description 26', '2003-02-01'),
('Book 27', 'Subtitle 27', 'Description 27', '2003-03-01'),
('Book 28', 'Subtitle 28', 'Description 28', '2003-04-01'),
('Book 29', 'Subtitle 29', 'Description 29', '2003-05-01'),
('Book 30', 'Subtitle 30', 'Description 30', '2003-06-01'),
('Book 31', 'Subtitle 31', 'Description 31', '2003-07-01'),
('Book 32', 'Subtitle 32', 'Description 32', '2003-08-01'),
('Book 33', 'Subtitle 33', 'Description 33', '2003-09-01'),
('Book 34', 'Subtitle 34', 'Description 34', '2003-10-01'),
('Book 35', 'Subtitle 35', 'Description 35', '2003-11-01'),
('Book 36', 'Subtitle 36', 'Description 36', '2003-12-01'),
('Book 37', 'Subtitle 37', 'Description 37', '2004-01-01'),
('Book 38', 'Subtitle 38', 'Description 38', '2004-02-01'),
('Book 39', 'Subtitle 39', 'Description 39', '2004-03-01'),
('Book 40', 'Subtitle 40', 'Description 40', '2004-04-01'),
('Book 41', 'Subtitle 41', 'Description 41', '2004-05-01'),
('Book 42', 'Subtitle 42', 'Description 42', '2004-06-01'),
('Book 43', 'Subtitle 43', 'Description 43', '2004-07-01'),
('Book 44', 'Subtitle 44', 'Description 44', '2004-08-01'),
('Book 45', 'Subtitle 45', 'Description 45', '2004-09-01'),
('Book 46', 'Subtitle 46', 'Description 46', '2004-10-01'),
('Book 47', 'Subtitle 47', 'Description 47', '2004-11-01'),
('Book 48', 'Subtitle 48', 'Description 48', '2004-12-01'),
('Book 49', 'Subtitle 49', 'Description 49', '2005-01-01'),
('Book 50', 'Subtitle 50', 'Description 50', '2005-02-01'),
('Book 51', 'Subtitle 51', 'Description 51', '2005-03-01'),
('Book 52', 'Subtitle 52', 'Description 52', '2005-04-01'),
('Book 53', 'Subtitle 53', 'Description 53', '2005-05-01'),
('Book 54', 'Subtitle 54', 'Description 54', '2005-06-01'),
('Book 55', 'Subtitle 55', 'Description 55', '2005-07-01'),
('Book 56', 'Subtitle 56', 'Description 56', '2005-08-01'),
('Book 57', 'Subtitle 57', 'Description 57', '2005-09-01'),
('Book 58', 'Subtitle 58', 'Description 58', '2005-10-01'),
('Book 59', 'Subtitle 59', 'Description 59', '2005-11-01'),
('Book 60', 'Subtitle 60', 'Description 60', '2005-12-01'),
('Book 61', 'Subtitle 61', 'Description 61', '2006-01-01'),
('Book 62', 'Subtitle 62', 'Description 62', '2006-02-01'),
('Book 63', 'Subtitle 63', 'Description 63', '2006-03-01'),
('Book 64', 'Subtitle 64', 'Description 64', '2006-04-01'),
('Book 65', 'Subtitle 65', 'Description 65', '2006-05-01'),
('Book 66', 'Subtitle 66', 'Description 66', '2006-06-01'),
('Book 67', 'Subtitle 67', 'Description 67', '2006-07-01'),
('Book 68', 'Subtitle 68', 'Description 68', '2006-08-01'),
('Book 69', 'Subtitle 69', 'Description 69', '2006-09-01'),
('Book 70', 'Subtitle 70', 'Description 70', '2006-10-01'),
('Book 71', 'Subtitle 71', 'Description 71', '2006-11-01'),
('Book 72', 'Subtitle 72', 'Description 72', '2006-12-01'),
('Book 73', 'Subtitle 73', 'Description 73', '2007-01-01'),
('Book 74', 'Subtitle 74', 'Description 74', '2007-02-01'),
('Book 75', 'Subtitle 75', 'Description 75', '2007-03-01'),
('Book 76', 'Subtitle 76', 'Description 76', '2007-04-01'),
('Book 77', 'Subtitle 77', 'Description 77', '2007-05-01'),
('Book 78', 'Subtitle 78', 'Description 78', '2007-06-01'),
('Book 79', 'Subtitle 79', 'Description 79', '2007-07-01'),
('Book 80', 'Subtitle 80', 'Description 80', '2007-08-01'),
('Book 81', 'Subtitle 81', 'Description 81', '2007-09-01'),
('Book 82', 'Subtitle 82', 'Description 82', '2007-10-01'),
('Book 83', 'Subtitle 83', 'Description 83', '2007-11-01'),
('Book 84', 'Subtitle 84', 'Description 84', '2007-12-01'),
('Book 85', 'Subtitle 85', 'Description 85', '2008-01-01'),
('Book 86', 'Subtitle 86', 'Description 86', '2008-02-01'),
('Book 87', 'Subtitle 87', 'Description 87', '2008-03-01'),
('Book 88', 'Subtitle 88', 'Description 88', '2008-04-01'),
('Book 89', 'Subtitle 89', 'Description 89', '2008-05-01'),
('Book 90', 'Subtitle 90', 'Description 90', '2008-06-01'),
('Book 91', 'Subtitle 91', 'Description 91', '2008-07-01'),
('Book 92', 'Subtitle 92', 'Description 92', '2008-08-01'),
('Book 93', 'Subtitle 93', 'Description 93', '2008-09-01'),
('Book 94', 'Subtitle 94', 'Description 94', '2008-10-01'),
('Book 95', 'Subtitle 95', 'Description 95', '2008-11-01'),
('Book 96', 'Subtitle 96', 'Description 96', '2008-12-01'),
('Book 97', 'Subtitle 97', 'Description 97', '2009-01-01'),
('Book 98', 'Subtitle 98', 'Description 98', '2009-02-01'),
('Book 99', 'Subtitle 99', 'Description 99', '2009-03-01'),
('Book 100', 'Subtitle 100', 'Description 100', '2009-04-01'),
('Book 101', 'Subtitle 101', 'Description 101', '2009-05-01'),
('Book 102', 'Subtitle 102', 'Description 102', '2009-06-01'),
('Book 103', 'Subtitle 103', 'Description 103', '2009-07-01'),
('Book 104', 'Subtitle 104', 'Description 104', '2009-08-01'),
('Book 105', 'Subtitle 105', 'Description 105', '2009-09-01'),
('Book 106', 'Subtitle 106', 'Description 106', '2009-10-01'),
('Book 107', 'Subtitle 107', 'Description 107', '2009-11-01'),
('Book 108', 'Subtitle 108', 'Description 108', '2009-12-01'),
('Book 109', 'Subtitle 109', 'Description 109', '2010-01-01'),
('Book 110', 'Subtitle 110', 'Description 110', '2010-02-01'),
('Book 111', 'Subtitle 111', 'Description 111', '2010-03-01'),
('Book 112', 'Subtitle 112', 'Description 112', '2010-04-01'),
('Book 113', 'Subtitle 113', 'Description 113', '2010-05-01'),
('Book 114', 'Subtitle 114', 'Description 114', '2010-06-01'),
('Book 115', 'Subtitle 115', 'Description 115', '2010-07-01'),
('Book 116', 'Subtitle 116', 'Description 116', '2010-08-01'),
('Book 117', 'Subtitle 117', 'Description 117', '2010-09-01'),
('Book 118', 'Subtitle 118', 'Description 118', '2010-10-01'),
('Book 119', 'Subtitle 119', 'Description 119', '2010-11-01'),
('Book 120', 'Subtitle 120', 'Description 120', '2010-12-01'),
('Book 121', 'Subtitle 121', 'Description 121', '2011-01-01'),
('Book 122', 'Subtitle 122', 'Description 122', '2011-02-01'),
('Book 123', 'Subtitle 123', 'Description 123', '2011-03-01'),
('Book 124', 'Subtitle 124', 'Description 124', '2011-04-01'),
('Book 125', 'Subtitle 125', 'Description 125', '2011-05-01'),
('Book 126', 'Subtitle 126', 'Description 126', '2011-06-01'),
('Book 127', 'Subtitle 127', 'Description 127', '2011-07-01'),
('Book 128', 'Subtitle 128', 'Description 128', '2011-08-01'),
('Book 129', 'Subtitle 129', 'Description 129', '2011-09-01'),
('Book 130', 'Subtitle 130', 'Description 130', '2011-10-01'),
('Book 131', 'Subtitle 131', 'Description 131', '2011-11-01'),
('Book 132', 'Subtitle 132', 'Description 132', '2011-12-01'),
('Book 133', 'Subtitle 133', 'Description 133', '2012-01-01'),
('Book 134', 'Subtitle 134', 'Description 134', '2012-02-01'),
('Book 135', 'Subtitle 135', 'Description 135', '2012-03-01'),
('Book 136', 'Subtitle 136', 'Description 136', '2012-04-01'),
('Book 137', 'Subtitle 137', 'Description 137', '2012-05-01'),
('Book 138', 'Subtitle 138', 'Description 138', '2012-06-01'),
('Book 139', 'Subtitle 139', 'Description 139', '2012-07-01'),
('Book 140', 'Subtitle 140', 'Description 140', '2012-08-01'),
('Book 141', 'Subtitle 141', 'Description 141', '2012-09-01'),
('Book 142', 'Subtitle 142', 'Description 142', '2012-10-01'),
('Book 143', 'Subtitle 143', 'Description 143', '2012-11-01'),
('Book 144', 'Subtitle 144', 'Description 144', '2012-12-01'),
('Book 145', 'Subtitle 145', 'Description 145', '2013-01-01'),
('Book 146', 'Subtitle 146', 'Description 146', '2013-02-01'),
('Book 147', 'Subtitle 147', 'Description 147', '2013-03-01'),
('Book 148', 'Subtitle 148', 'Description 148', '2013-04-01'),
('Book 149', 'Subtitle 149', 'Description 149', '2013-05-01'),
('Book 150', 'Subtitle 150', 'Description 150', '2013-06-01'),
('Book 151', 'Subtitle 151', 'Description 151', '2013-07-01'),
('Book 152', 'Subtitle 152', 'Description 152', '2013-08-01'),
('Book 153', 'Subtitle 153', 'Description 153', '2013-09-01'),
('Book 154', 'Subtitle 154', 'Description 154', '2013-10-01'),
('Book 155', 'Subtitle 155', 'Description 155', '2013-11-01'),
('Book 156', 'Subtitle 156', 'Description 156', '2013-12-01'),
('Book 157', 'Subtitle 157', 'Description 157', '2014-01-01'),
('Book 158', 'Subtitle 158', 'Description 158', '2014-02-01'),
('Book 159', 'Subtitle 159', 'Description 159', '2014-03-01'),
('Book 160', 'Subtitle 160', 'Description 160', '2014-04-01'),
('Book 161', 'Subtitle 161', 'Description 161', '2014-05-01'),
('Book 162', 'Subtitle 162', 'Description 162', '2014-06-01'),
('Book 163', 'Subtitle 163', 'Description 163', '2014-07-01'),
('Book 164', 'Subtitle 164', 'Description 164', '2014-08-01'),
('Book 165', 'Subtitle 165', 'Description 165', '2014-09-01'),
('Book 166', 'Subtitle 166', 'Description 166', '2014-10-01'),
('Book 167', 'Subtitle 167', 'Description 167', '2014-11-01'),
('Book 168', 'Subtitle 168', 'Description 168', '2014-12-01'),
('Book 169', 'Subtitle 169', 'Description 169', '2015-01-01'),
('Book 170', 'Subtitle 170', 'Description 170', '2015-02-01'),
('Book 171', 'Subtitle 171', 'Description 171', '2015-03-01'),
('Book 172', 'Subtitle 172', 'Description 172', '2015-04-01'),
('Book 173', 'Subtitle 173', 'Description 173', '2015-05-01'),
('Book 174', 'Subtitle 174', 'Description 174', '2015-06-01'),
('Book 175', 'Subtitle 175', 'Description 175', '2015-07-01'),
('Book 176', 'Subtitle 176', 'Description 176', '2015-08-01'),
('Book 177', 'Subtitle 177', 'Description 177', '2015-09-01'),
('Book 178', 'Subtitle 178', 'Description 178', '2015-10-01'),
('Book 179', 'Subtitle 179', 'Description 179', '2015-11-01'),
('Book 180', 'Subtitle 180', 'Description 180', '2015-12-01'),
('Book 181', 'Subtitle 181', 'Description 181', '2016-01-01'),
('Book 182', 'Subtitle 182', 'Description 182', '2016-02-01'),
('Book 183', 'Subtitle 183', 'Description 183', '2016-03-01'),
('Book 184', 'Subtitle 184', 'Description 184', '2016-04-01'),
('Book 185', 'Subtitle 185', 'Description 185', '2016-05-01'),
('Book 186', 'Subtitle 186', 'Description 186', '2016-06-01'),
('Book 187', 'Subtitle 187', 'Description 187', '2016-07-01'),
('Book 188', 'Subtitle 188', 'Description 188', '2016-08-01'),
('Book 189', 'Subtitle 189', 'Description 189', '2016-09-01'),
('Book 190', 'Subtitle 190', 'Description 190', '2016-10-01'),
('Book 191', 'Subtitle 191', 'Description 191', '2016-11-01'),
('Book 192', 'Subtitle 192', 'Description 192', '2016-12-01'),
('Book 193', 'Subtitle 193', 'Description 193', '2017-01-01'),
('Book 194', 'Subtitle 194', 'Description 194', '2017-02-01'),
('Book 195', 'Subtitle 195', 'Description 195', '2017-03-01'),
('Book 196', 'Subtitle 196', 'Description 196', '2017-04-01'),
('Book 197', 'Subtitle 197', 'Description 197', '2017-05-01'),
('Book 198', 'Subtitle 198', 'Description 198', '2017-06-01'),
('Book 199', 'Subtitle 199', 'Description 199', '2017-07-01'),
('Book 200', 'Subtitle 200', 'Description 200', '2017-08-01');


INSERT INTO "ExhibitionBooks" ("ExhibitionId", "BookId") 
VALUES
(1, 1),
(1, 2),
(1, 6);


INSERT INTO "ExhibitionBooks" ("ExhibitionId", "BookId") 
VALUES
(2, 5),
(2, 7),
(2, 8);


INSERT INTO "ExhibitionBooks" ("ExhibitionId", "BookId") 
VALUES
(3, 3), 
(3, 9), 
(3, 1);


INSERT INTO "Covers" ("Description", "ImageUrl", "BookId")  
VALUES 
('Front cover of Harry Potter', 'https://m.media-amazon.com/images/I/81YOuOGFCJL.jpg', 1),
('Back cover of Harry Potter', 'https://m.media-amazon.com/images/I/51UoqRAxwEL._SX331_BO1,204,203,200_.jpg', 1),
('Special edition cover of Harry Potter', 'https://m.media-amazon.com/images/I/91ocU8970hL.jpg', 1);

INSERT INTO "Covers" ("Description", "ImageUrl", "BookId")  
VALUES 
('Front cover of The Hobbit', 'https://m.media-amazon.com/images/I/91b0C2YNSrL.jpg', 2),
('Back cover of The Hobbit', 'https://m.media-amazon.com/images/I/81t2CVWEsUL.jpg', 2);

INSERT INTO "Covers" ("Description", "ImageUrl", "BookId")  
VALUES 
('Front cover of 1984', 'https://m.media-amazon.com/images/I/71kxa1-0mfL.jpg', 3),
('Back cover of 1984', 'https://m.media-amazon.com/images/I/81SZ3X7MB4L.jpg', 3),
('Collectors edition cover of 1984', 'https://m.media-amazon.com/images/I/81tEgsxpNZS.jpg', 3);

INSERT INTO "Covers" ("Description", "ImageUrl", "BookId")  
VALUES 
('Front cover of To Kill a Mockingbird', 'https://m.media-amazon.com/images/I/81gepf1eMqL.jpg', 4);



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
(1, 4), (2, 2), (3, 8), (4, 5), (5, 1),
(6, 6), (7, 3), (8, 7), (9, 2), (10, 1),
(11, 4), (12, 6), (13, 5), (14, 2), (15, 7),
(16, 1), (17, 8), (18, 4), (19, 3), (20, 6),
(21, 2), (22, 7), (23, 1), (24, 5), (25, 3),
(26, 8), (27, 4), (28, 6), (29, 2), (30, 1),
(31, 7), (32, 3), (33, 5), (34, 4), (35, 8),
(36, 2), (37, 1), (38, 6), (39, 5), (40, 7),
(41, 4), (42, 8), (43, 3), (44, 1), (45, 6),
(46, 2), (47, 7), (48, 5), (49, 4), (50, 1),
(51, 3), (52, 6), (53, 2), (54, 5), (55, 8),
(56, 1), (57, 4), (58, 6), (59, 3), (60, 2),
(61, 8), (62, 5), (63, 1), (64, 7), (65, 3),
(66, 6), (67, 4), (68, 2), (69, 1), (70, 8),
(71, 5), (72, 6), (73, 3), (74, 7), (75, 2),
(76, 1), (77, 4), (78, 6), (79, 3), (80, 5),
(81, 8), (82, 2), (83, 7), (84, 1), (85, 4),
(86, 3), (87, 5), (88, 6), (89, 1), (90, 8),
(91, 7), (92, 2), (93, 4), (94, 5), (95, 1),
(96, 3), (97, 6), (98, 8), (99, 2), (100, 4),
(101, 5), (102, 3), (103, 1), (104, 6), (105, 8),
(106, 2), (107, 4), (108, 7), (109, 5), (110, 1),
(111, 3), (112, 6), (113, 4), (114, 8), (115, 2),
(116, 1), (117, 3), (118, 6), (119, 5), (120, 7),
(121, 8), (122, 4), (123, 1), (124, 5), (125, 2),
(126, 3), (127, 6), (128, 1), (129, 7), (130, 4),
(131, 8), (132, 5), (133, 2), (134, 1), (135, 3),
(136, 6), (137, 4), (138, 8), (139, 7), (140, 5),
(141, 2), (142, 1), (143, 6), (144, 3), (145, 4),
(146, 7), (147, 8), (148, 2), (149, 5), (150, 1),
(151, 3), (152, 6), (153, 4), (154, 1), (155, 8),
(156, 7), (157, 5), (158, 2), (159, 3), (160, 4),
(161, 1), (162, 8), (163, 5), (164, 6), (165, 3),
(166, 2), (167, 1), (168, 7), (169, 4), (170, 8),
(171, 5), (172, 3), (173, 6), (174, 1), (175, 2),
(176, 4), (177, 7), (178, 8), (179, 5), (180, 6),
(181, 3), (182, 1), (183, 2), (184, 4), (185, 5),
(186, 6), (187, 3), (188, 1), (189, 8), (190, 7),
(191, 2), (192, 5), (193, 4), (194, 1), (195, 6),
(196, 8), (197, 3), (198, 2), (199, 5), (200, 4);


INSERT INTO "BookSubjects" ("BookId", "SubjectId")
VALUES
(1, 5), (2, 2), (3, 7), (4, 3), (5, 1),
(6, 6), (7, 4), (8, 8), (9, 5), (10, 1),
(11, 3), (12, 7), (13, 6), (14, 4), (15, 2),
(16, 1), (17, 8), (18, 5), (19, 3), (20, 6),
(21, 2), (22, 7), (23, 4), (24, 1), (25, 8),
(26, 3), (27, 5), (28, 6), (29, 2), (30, 4),
(31, 7), (32, 1), (33, 8), (34, 5), (35, 3),
(36, 2), (37, 4), (38, 6), (39, 1), (40, 7),
(41, 8), (42, 3), (43, 2), (44, 5), (45, 6),
(46, 4), (47, 1), (48, 7), (49, 8), (50, 5),
(51, 2), (52, 6), (53, 3), (54, 1), (55, 4),
(56, 7), (57, 8), (58, 5), (59, 2), (60, 3),
(61, 6), (62, 1), (63, 4), (64, 7), (65, 8),
(66, 2), (67, 5), (68, 3), (69, 6), (70, 4),
(71, 1), (72, 8), (73, 7), (74, 5), (75, 2),
(76, 3), (77, 1), (78, 6), (79, 4), (80, 8),
(81, 7), (82, 5), (83, 2), (84, 1), (85, 3),
(86, 6), (87, 4), (88, 8), (89, 5), (90, 2),
(91, 1), (92, 7), (93, 3), (94, 6), (95, 4),
(96, 5), (97, 1), (98, 2), (99, 8), (100, 6),
(101, 7), (102, 3), (103, 4), (104, 2), (105, 5),
(106, 1), (107, 8), (108, 6), (109, 3), (110, 2),
(111, 7), (112, 4), (113, 5), (114, 1), (115, 8),
(116, 3), (117, 2), (118, 6), (119, 7), (120, 4),
(121, 8), (122, 5), (123, 1), (124, 3), (125, 2),
(126, 4), (127, 6), (128, 5), (129, 8), (130, 7),
(131, 1), (132, 3), (133, 4), (134, 2), (135, 6),
(136, 5), (137, 8), (138, 1), (139, 7), (140, 2),
(141, 6), (142, 3), (143, 4), (144, 8), (145, 1),
(146, 5), (147, 2), (148, 7), (149, 3), (150, 6),
(151, 4), (152, 1), (153, 8), (154, 5), (155, 2),
(156, 6), (157, 3), (158, 4), (159, 1), (160, 7),
(161, 8), (162, 5), (163, 2), (164, 3), (165, 4),
(166, 1), (167, 6), (168, 5), (169, 7), (170, 2),
(171, 3), (172, 8), (173, 1), (174, 6), (175, 4),
(176, 2), (177, 5), (178, 7), (179, 8), (180, 1),
(181, 6), (182, 3), (183, 2), (184, 4), (185, 5),
(186, 1), (187, 8), (188, 6), (189, 3), (190, 7),
(191, 2), (192, 5), (193, 1), (194, 4), (195, 8),
(196, 6), (197, 3), (198, 2), (199, 5), (200, 7);


INSERT INTO "Issues" ("ClientCardId","ClientCardId1","ClientCardId2", "BookId", "IssueFrom", "IssueTo", "ReturnDate") 
VALUES 
(1,1,1, 1, '2024-10-01', '2024-10-15', '2024-10-10'),
(2,2,2, 2, '2024-10-02', '2024-10-16', NULL),
(3,3,3, 3, '2024-10-03', '2024-10-17', '2024-10-14'),
(4,4,4, 4, '2024-10-04', '2024-10-18', NULL),
(5,5,5, 5, '2024-10-05', '2024-10-19', '2024-10-18'),
(6,6,6, 6, '2024-10-06', '2024-10-20', NULL),
(7,7,7, 7, '2024-10-07', '2024-10-21', '2024-10-20'),
(8,8,8, 8, '2024-10-08', '2024-10-22', NULL),
(9,9,9, 9, '2024-10-09', '2024-10-23', '2024-10-22'),
(10,10,10, 10, '2024-10-10', '2024-10-24', NULL);