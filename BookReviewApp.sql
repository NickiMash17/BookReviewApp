-- DROP TABLES IF THEY EXIST (in reverse order of creation to avoid constraint violations)
IF OBJECT_ID('Reviews', 'U') IS NOT NULL DROP TABLE Reviews;
IF OBJECT_ID('Books', 'U') IS NOT NULL DROP TABLE Books;
IF OBJECT_ID('Authors', 'U') IS NOT NULL DROP TABLE Authors;

-- CREATE TABLES
-- Authors Table
CREATE TABLE Authors (
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Biography NVARCHAR(MAX),
    DateOfBirth DATE,
    PhotoUrl NVARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Books Table
CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ISBN NVARCHAR(20),
    PublishedDate DATE,
    Price DECIMAL(10, 2),
    CoverImageUrl NVARCHAR(500),
    AuthorId INT FOREIGN KEY REFERENCES Authors(AuthorId),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Reviews Table
CREATE TABLE Reviews (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    BookId INT FOREIGN KEY REFERENCES Books(BookId),
    ReviewerName NVARCHAR(100),
    ReviewDate DATE,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- INSERT AUTHORS
INSERT INTO Authors (Name, Biography, DateOfBirth, PhotoUrl, CreatedAt)
VALUES 
-- Original Authors
('J.K. Rowling', 'British author best known for the Harry Potter series', '1965-07-31', 'https://upload.wikimedia.org/wikipedia/commons/5/5d/J._K._Rowling_2010.jpg', GETDATE()),
('Stephen King', 'American author of horror, supernatural fiction, suspense', '1947-09-21', 'https://upload.wikimedia.org/wikipedia/commons/e/e3/Stephen_King%2C_Comicon.jpg', GETDATE()),
('Agatha Christie', 'English writer known for her detective novels', '1890-09-15', 'https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png', GETDATE()),
-- Additional Authors
('George R.R. Martin', 'American novelist best known for A Song of Ice and Fire series', '1948-09-20', 'https://upload.wikimedia.org/wikipedia/commons/e/ed/George_R.R._Martin_by_Gage_Skidmore_2.jpg', GETDATE()),
('Jane Austen', 'English novelist known for her romantic fiction', '1775-12-16', 'https://upload.wikimedia.org/wikipedia/commons/c/cc/CassandraAusten-JaneAusten%28c.1810%29_hires.jpg', GETDATE()),
('Haruki Murakami', 'Japanese writer known for his surrealist novels', '1949-01-12', 'https://upload.wikimedia.org/wikipedia/commons/e/eb/Murakami_Haruki_%282009%29.jpg', GETDATE()),
('Margaret Atwood', 'Canadian poet and novelist', '1939-11-18', 'https://upload.wikimedia.org/wikipedia/commons/7/75/Margaret_Atwood_2015.jpg', GETDATE()),
('Neil Gaiman', 'English author of short fiction, novels, comic books, and films', '1960-11-10', 'https://upload.wikimedia.org/wikipedia/commons/b/bc/Kyle_Cassidy_-_Neil_Gaiman_2013-2.jpg', GETDATE()),
('Gabriel García Márquez', 'Colombian novelist known for magical realism', '1927-03-06', 'https://upload.wikimedia.org/wikipedia/commons/0/0f/Gabriel_Garcia_Marquez.jpg', GETDATE()),
('Toni Morrison', 'American novelist and Nobel Prize winner', '1931-02-18', 'https://upload.wikimedia.org/wikipedia/commons/8/8c/Toni_Morrison_2008-2.jpg', GETDATE()),
('Ernest Hemingway', 'American novelist and short story writer', '1899-07-21', 'https://upload.wikimedia.org/wikipedia/commons/2/28/ErnestHemingway.jpg', GETDATE()),
('Virginia Woolf', 'English writer and modernist', '1882-01-25', 'https://upload.wikimedia.org/wikipedia/commons/0/0b/George_Charles_Beresford_-_Virginia_Woolf_in_1902_-_Restoration.jpg', GETDATE());

-- INSERT BOOKS
INSERT INTO Books (Title, Description, ISBN, PublishedDate, Price, CoverImageUrl, AuthorId, CreatedAt)
VALUES 
-- Original Books
('Harry Potter and the Philosopher''s Stone', 'The first book in the Harry Potter series', '9780747532743', '1997-06-26', 12.99, 'https://m.media-amazon.com/images/I/81m1s4wIPML._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
('The Shining', 'A horror novel about a haunted hotel', '9780307743657', '1977-01-28', 9.99, 'https://m.media-amazon.com/images/I/71BPuv+iRbL._AC_UF1000,1000_QL80_.jpg', 2, DATEADD(SECOND, 5, GETDATE())),
('Murder on the Orient Express', 'A detective novel featuring Hercule Poirot', '9780007119318', '1934-01-01', 8.50, 'https://m.media-amazon.com/images/I/91oK9tS7EfL._AC_UF1000,1000_QL80_.jpg', 3, DATEADD(SECOND, 10, GETDATE())),
-- Additional Books
('A Game of Thrones', 'The first book in A Song of Ice and Fire series', '9780553573404', '1996-08-06', 14.99, 'https://m.media-amazon.com/images/I/91dSMhdIzTL._AC_UF1000,1000_QL80_.jpg', 4, GETDATE()),
('Pride and Prejudice', 'A romantic novel of manners', '9780141439518', '1813-01-28', 7.99, 'https://m.media-amazon.com/images/I/71Q1tPupKjL._AC_UF1000,1000_QL80_.jpg', 5, DATEADD(SECOND, 5, GETDATE())),
('Norwegian Wood', 'A nostalgic story of loss and sexuality', '9780375704024', '1987-09-04', 10.50, 'https://m.media-amazon.com/images/I/81Fsg8ZcOMC._AC_UF1000,1000_QL80_.jpg', 6, DATEADD(SECOND, 10, GETDATE())),
('The Handmaid''s Tale', 'A dystopian novel set in a totalitarian society', '9780385490818', '1985-06-01', 11.99, 'https://m.media-amazon.com/images/I/91vUn7t3zGL._AC_UF1000,1000_QL80_.jpg', 7, DATEADD(SECOND, 15, GETDATE())),
('American Gods', 'A novel about a war between old and new gods', '9780062572110', '2001-06-19', 12.50, 'https://m.media-amazon.com/images/I/81vOOCrKCLL._AC_UF1000,1000_QL80_.jpg', 8, DATEADD(SECOND, 20, GETDATE())),
('Harry Potter and the Chamber of Secrets', 'The second book in the Harry Potter series', '9780747538486', '1998-07-02', 12.99, 'https://m.media-amazon.com/images/I/91OINeHnJGL._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
-- Even More Books
('One Hundred Years of Solitude', 'A landmark novel of magical realism', '9780060883287', '1967-05-30', 11.99, 'https://m.media-amazon.com/images/I/91hJ+hgZm4L._AC_UF1000,1000_QL80_.jpg', 9, GETDATE()),
('Beloved', 'A powerful examination of slavery, family, and memory', '9781400033416', '1987-09-02', 10.99, 'https://m.media-amazon.com/images/I/71tZrHXQ+YL._AC_UF1000,1000_QL80_.jpg', 10, GETDATE()),
('The Old Man and the Sea', 'A short novel about an aging Cuban fisherman', '9780684801223', '1952-09-01', 8.99, 'https://m.media-amazon.com/images/I/71KloredA3L._AC_UF1000,1000_QL80_.jpg', 11, GETDATE()),
('To the Lighthouse', 'A modernist novel examining family relationships', '9780156907392', '1927-05-05', 9.50, 'https://m.media-amazon.com/images/I/71YoFhT+9eL._AC_UF1000,1000_QL80_.jpg', 12, GETDATE()),
('Harry Potter and the Prisoner of Azkaban', 'The third book in the Harry Potter series', '9780747546290', '1999-07-08', 13.99, 'https://m.media-amazon.com/images/I/91Z9xnjKnYL._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
('It', 'A horror novel about an ancient, shape-shifting evil', '9781501142970', '1986-09-15', 12.99, 'https://m.media-amazon.com/images/I/71tFhdcC0XL._AC_UF1000,1000_QL80_.jpg', 2, GETDATE()),
('Death on the Nile', 'A mystery novel featuring detective Hercule Poirot', '9780062073556', '1937-11-01', 8.99, 'https://m.media-amazon.com/images/I/71+ws7RD3mL._AC_UF1000,1000_QL80_.jpg', 3, GETDATE());

-- INSERT REVIEWS
INSERT INTO Reviews (Rating, Comment, BookId, ReviewerName, ReviewDate, CreatedAt)
VALUES 
-- Original Reviews
(5, 'Changed my childhood! Magical and wonderful.', 1, 'BookLover42', '2020-05-15', GETDATE()),
(4, 'Scared me to death but couldn''t put it down!', 2, 'HorrorFan', '2019-10-31', DATEADD(SECOND, 5, GETDATE())),
(5, 'Brilliant mystery with an unforgettable ending', 3, 'MysteryReader', '2021-03-22', DATEADD(SECOND, 10, GETDATE())),
-- Additional Reviews
(5, 'Epic fantasy at its best. Intricate characters and brutal plot twists.', 4, 'FantasyFan83', '2022-01-10', GETDATE()),
(4, 'A timeless classic with witty dialogue and memorable characters.', 5, 'LiteratureLover', '2021-07-22', DATEADD(SECOND, 5, GETDATE())),
(5, 'Beautifully written and melancholic. Murakami at his finest.', 6, 'JapaneseLitFan', '2022-04-30', DATEADD(SECOND, 10, GETDATE())),
(4, 'Chilling and thought-provoking. Even more relevant today.', 7, 'DystopiaReader', '2021-11-15', DATEADD(SECOND, 15, GETDATE())),
(5, 'A masterpiece of modern mythology. Gaiman blends fantasy and reality perfectly.', 8, 'MythologyBuff', '2022-02-28', DATEADD(SECOND, 20, GETDATE())),
(4, 'The series gets even better! Loved the mystery aspect of this one.', 9, 'PotterHead', '2020-06-12', GETDATE()),
-- Even More Reviews
(5, 'A masterpiece of magical realism. Rich and captivating storytelling.', 10, 'LiteraryExplorer', '2022-03-18', GETDATE()),
(5, 'Haunting and powerful. Morrison''s prose is unmatched.', 11, 'BookwormElite', '2021-09-05', GETDATE()),
(4, 'Simple yet profound. Hemingway''s sparse style at its best.', 12, 'ClassicsFan', '2022-01-15', GETDATE()),
(4, 'Woolf''s stream of consciousness writing is hypnotic and immersive.', 13, 'ModernistReader', '2021-11-30', GETDATE()),
(5, 'The best in the series! Sirius Black is my favorite character.', 14, 'WizardingFan', '2020-08-10', GETDATE()),
(5, 'King''s masterpiece. Pennywise will haunt your dreams forever.', 15, 'HorrorObsessed', '2021-10-25', GETDATE()),
(4, 'Christie at her best! The twist ending is exceptional.', 16, 'MysteryAddict', '2022-02-05', GETDATE()),
-- Additional Reviews for Existing Books
(5, 'Re-read this after 20 years and it''s still perfect!', 1, 'RetroReader', '2022-04-10', GETDATE()),
(3, 'A bit slow at times but the ending makes up for it.', 2, 'CasualReader', '2021-12-05', GETDATE()),
(5, 'Christie''s most iconic work for good reason.', 3, 'GoldenAgeFan', '2022-03-01', GETDATE()),
(4, 'Not perfect but launched an amazing series.', 4, 'WesterosLover', '2021-08-12', GETDATE()),
(5, 'A perfect romance novel that stands the test of time.', 5, 'RomanceReader', '2022-01-22', GETDATE());

-- VERIFY DATA
SELECT COUNT(*) AS TotalAuthors FROM Authors;
SELECT COUNT(*) AS TotalBooks FROM Books;
SELECT COUNT(*) AS TotalReviews FROM Reviews;

-- USEFUL QUERIES FOR DATABASE
-- Books by Author
SELECT a.Name AS Author, COUNT(b.BookId) AS NumberOfBooks
FROM Authors a
LEFT JOIN Books b ON a.AuthorId = b.AuthorId
GROUP BY a.Name
ORDER BY NumberOfBooks DESC;

-- Books with Ratings
SELECT b.Title, a.Name AS Author, AVG(r.Rating) AS AverageRating, COUNT(r.ReviewId) AS NumberOfReviews
FROM Books b
JOIN Authors a ON b.AuthorId = a.AuthorId
LEFT JOIN Reviews r ON b.BookId = r.BookId
GROUP BY b.Title, a.Name
ORDER BY AverageRating DESC;

-- Most Reviewed Books
SELECT b.Title, a.Name AS Author, COUNT(r.ReviewId) AS NumberOfReviews
FROM Books b
JOIN Authors a ON b.AuthorId = a.AuthorId
LEFT JOIN Reviews r ON b.BookId = r.BookId
GROUP BY b.Title, a.Name
ORDER BY NumberOfReviews DESC;

-- Books Published by Decade
SELECT 
    CONCAT(CAST(YEAR(b.PublishedDate) / 10 * 10 AS VARCHAR), 's') AS Decade,
    COUNT(b.BookId) AS NumberOfBooks
FROM Books b
GROUP BY YEAR(b.PublishedDate) / 10
ORDER BY MIN(YEAR(b.PublishedDate));