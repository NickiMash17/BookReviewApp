-- BookReviewDB Setup Script
-- Drops, creates, and populates the Authors, Books, and Reviews tables

-- =========================
-- 1. DROP TABLES IF THEY EXIST (in correct order)
-- =========================
DROP TABLE IF EXISTS Reviews;
DROP TABLE IF EXISTS Books;
DROP TABLE IF EXISTS Authors;

-- =========================
-- 2. CREATE TABLES
-- =========================

-- Authors Table
CREATE TABLE Authors
(
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Biography NVARCHAR(MAX),
    DateOfBirth DATE,
    PhotoUrl NVARCHAR(500),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);

-- Books Table
CREATE TABLE Books
(
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ISBN NVARCHAR(20),
    PublishedDate DATE,
    Price DECIMAL(10, 2),
    CoverImageUrl NVARCHAR(500),
    AuthorId INT NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Books_Authors FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

-- Reviews Table
CREATE TABLE Reviews
(
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    BookId INT NOT NULL,
    ReviewerName NVARCHAR(100),
    ReviewDate DATE,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Reviews_Books FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

-- =========================
-- 3. INSERT AUTHORS
-- =========================
INSERT INTO Authors (Name, Biography, DateOfBirth, PhotoUrl, CreatedAt) VALUES
    (N'J.K. Rowling', N'British author best known for the Harry Potter series', '1965-07-31', N'https://upload.wikimedia.org/wikipedia/commons/5/5d/J._K._Rowling_2010.jpg', GETDATE()),
    (N'Stephen King', N'American author of horror, supernatural fiction, suspense', '1947-09-21', N'https://upload.wikimedia.org/wikipedia/commons/e/e3/Stephen_King%2C_Comicon.jpg', GETDATE()),
    (N'Agatha Christie', N'English writer known for her detective novels', '1890-09-15', N'https://upload.wikimedia.org/wikipedia/commons/c/cf/Agatha_Christie.png', GETDATE()),
    (N'George R.R. Martin', N'American novelist best known for A Song of Ice and Fire series', '1948-09-20', N'https://upload.wikimedia.org/wikipedia/commons/e/ed/George_R.R._Martin_by_Gage_Skidmore_2.jpg', GETDATE()),
    (N'Jane Austen', N'English novelist known for her romantic fiction', '1775-12-16', N'https://upload.wikimedia.org/wikipedia/commons/c/cc/CassandraAusten-JaneAusten%28c.1810%29_hires.jpg', GETDATE()),
    (N'Haruki Murakami', N'Japanese writer known for his surrealist novels', '1949-01-12', N'https://upload.wikimedia.org/wikipedia/commons/e/eb/Murakami_Haruki_%282009%29.jpg', GETDATE()),
    (N'Margaret Atwood', N'Canadian poet and novelist', '1939-11-18', N'https://upload.wikimedia.org/wikipedia/commons/7/75/Margaret_Atwood_2015.jpg', GETDATE()),
    (N'Neil Gaiman', N'English author of short fiction, novels, comic books, and films', '1960-11-10', N'https://upload.wikimedia.org/wikipedia/commons/b/bc/Kyle_Cassidy_-_Neil_Gaiman_2013-2.jpg', GETDATE()),
    (N'Gabriel García Márquez', N'Colombian novelist known for magical realism', '1927-03-06', N'https://upload.wikimedia.org/wikipedia/commons/0/0f/Gabriel_Garcia_Marquez.jpg', GETDATE()),
    (N'Toni Morrison', N'American novelist and Nobel Prize winner', '1931-02-18', N'https://upload.wikimedia.org/wikipedia/commons/8/8c/Toni_Morrison_2008-2.jpg', GETDATE()),
    (N'Ernest Hemingway', N'American novelist and short story writer', '1899-07-21', N'https://upload.wikimedia.org/wikipedia/commons/2/28/ErnestHemingway.jpg', GETDATE()),
    (N'Virginia Woolf', N'English writer and modernist', '1882-01-25', N'https://upload.wikimedia.org/wikipedia/commons/0/0b/George_Charles_Beresford_-_Virginia_Woolf_in_1902_-_Restoration.jpg', GETDATE());

-- =========================
-- 4. INSERT BOOKS
-- =========================
INSERT INTO Books (Title, Description, ISBN, PublishedDate, Price, CoverImageUrl, AuthorId, CreatedAt) VALUES
    (N'Harry Potter and the Philosopher''s Stone', N'The first book in the Harry Potter series', N'9780747532743', '1997-06-26', 12.99, N'https://m.media-amazon.com/images/I/81m1s4wIPML._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
    (N'The Shining', N'A horror novel about a haunted hotel', N'9780307743657', '1977-01-28', 9.99, N'https://m.media-amazon.com/images/I/71BPuv+iRbL._AC_UF1000,1000_QL80_.jpg', 2, DATEADD(SECOND, 5, GETDATE())),
    (N'Murder on the Orient Express', N'A detective novel featuring Hercule Poirot', N'9780007119318', '1934-01-01', 8.50, N'https://m.media-amazon.com/images/I/91oK9tS7EfL._AC_UF1000,1000_QL80_.jpg', 3, DATEADD(SECOND, 10, GETDATE())),
    (N'A Game of Thrones', N'The first book in A Song of Ice and Fire series', N'9780553573404', '1996-08-06', 14.99, N'https://m.media-amazon.com/images/I/91dSMhdIzTL._AC_UF1000,1000_QL80_.jpg', 4, GETDATE()),
    (N'Pride and Prejudice', N'A romantic novel of manners', N'9780141439518', '1813-01-28', 7.99, N'https://m.media-amazon.com/images/I/71Q1tPupKjL._AC_UF1000,1000_QL80_.jpg', 5, DATEADD(SECOND, 5, GETDATE())),
    (N'Norwegian Wood', N'A nostalgic story of loss and sexuality', N'9780375704024', '1987-09-04', 10.50, N'https://m.media-amazon.com/images/I/81Fsg8ZcOMC._AC_UF1000,1000_QL80_.jpg', 6, DATEADD(SECOND, 10, GETDATE())),
    (N'The Handmaid''s Tale', N'A dystopian novel set in a totalitarian society', N'9780385490818', '1985-06-01', 11.99, N'https://m.media-amazon.com/images/I/91vUn7t3zGL._AC_UF1000,1000_QL80_.jpg', 7, DATEADD(SECOND, 15, GETDATE())),
    (N'American Gods', N'A novel about a war between old and new gods', N'9780062572110', '2001-06-19', 12.50, N'https://m.media-amazon.com/images/I/81vOOCrKCLL._AC_UF1000,1000_QL80_.jpg', 8, DATEADD(SECOND, 20, GETDATE())),
    (N'Harry Potter and the Chamber of Secrets', N'The second book in the Harry Potter series', N'9780747538486', '1998-07-02', 12.99, N'https://m.media-amazon.com/images/I/91OINeHnJGL._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
    (N'One Hundred Years of Solitude', N'A landmark novel of magical realism', N'9780060883287', '1967-05-30', 11.99, N'https://m.media-amazon.com/images/I/91hJ+hgZm4L._AC_UF1000,1000_QL80_.jpg', 9, GETDATE()),
    (N'Beloved', N'A powerful examination of slavery, family, and memory', N'9781400033416', '1987-09-02', 10.99, N'https://m.media-amazon.com/images/I/71tZrHXQ+YL._AC_UF1000,1000_QL80_.jpg', 10, GETDATE()),
    (N'The Old Man and the Sea', N'A short novel about an aging Cuban fisherman', N'9780684801223', '1952-09-01', 8.99, N'https://m.media-amazon.com/images/I/71KloredA3L._AC_UF1000,1000_QL80_.jpg', 11, GETDATE()),
    (N'To the Lighthouse', N'A modernist novel examining family relationships', N'9780156907392', '1927-05-05', 9.50, N'https://m.media-amazon.com/images/I/71YoFhT+9eL._AC_UF1000,1000_QL80_.jpg', 12, GETDATE()),
    (N'Harry Potter and the Prisoner of Azkaban', N'The third book in the Harry Potter series', N'9780747546290', '1999-07-08', 13.99, N'https://m.media-amazon.com/images/I/91Z9xnjKnYL._AC_UF1000,1000_QL80_.jpg', 1, GETDATE()),
    (N'It', N'A horror novel about an ancient, shape-shifting evil', N'9781501142970', '1986-09-15', 12.99, N'https://m.media-amazon.com/images/I/71tFhdcC0XL._AC_UF1000,1000_QL80_.jpg', 2, GETDATE()),
    (N'Death on the Nile', N'A mystery novel featuring detective Hercule Poirot', N'9780062073556', '1937-11-01', 8.99, N'https://m.media-amazon.com/images/I/71+ws7RD3mL._AC_UF1000,1000_QL80_.jpg', 3, GETDATE());

-- =========================
-- 5. INSERT REVIEWS
-- =========================
INSERT INTO Reviews (Rating, Comment, BookId, ReviewerName, ReviewDate, CreatedAt) VALUES
    (5, N'Changed my childhood! Magical and wonderful.', 1, N'BookLover42', '2020-05-15', GETDATE()),
    (4, N'Scared me to death but couldn''t put it down!', 2, N'HorrorFan', '2019-10-31', DATEADD(SECOND, 5, GETDATE())),
    (5, N'Brilliant mystery with an unforgettable ending', 3, N'MysteryReader', '2021-03-22', DATEADD(SECOND, 10, GETDATE())),
    (5, N'Epic fantasy at its best. Intricate characters and brutal plot twists.', 4, N'FantasyFan83', '2022-01-10', GETDATE()),
    (4, N'A timeless classic with witty dialogue and memorable characters.', 5, N'LiteratureLover', '2021-07-22', DATEADD(SECOND, 5, GETDATE())),
    (5, N'Beautifully written and melancholic. Murakami at his finest.', 6, N'JapaneseLitFan', '2022-04-30', DATEADD(SECOND, 10, GETDATE())),
    (4, N'Chilling and thought-provoking. Even more relevant today.', 7, N'DystopiaReader', '2021-11-15', DATEADD(SECOND, 15, GETDATE())),
    (5, N'A masterpiece of modern mythology. Gaiman blends fantasy and reality perfectly.', 8, N'MythologyBuff', '2022-02-28', DATEADD(SECOND, 20, GETDATE())),
    (4, N'The series gets even better! Loved the mystery aspect of this one.', 9, N'PotterHead', '2020-06-12', GETDATE()),
    (5, N'A masterpiece of magical realism. Rich and captivating storytelling.', 10, N'LiteraryExplorer', '2022-03-18', GETDATE()),
    (5, N'Haunting and powerful. Morrison''s prose is unmatched.', 11, N'BookwormElite', '2021-09-05', GETDATE()),
    (4, N'Simple yet profound. Hemingway''s sparse style at its best.', 12, N'ClassicsFan', '2022-01-15', GETDATE()),
    (4, N'Woolf''s stream of consciousness writing is hypnotic and immersive.', 13, N'ModernistReader', '2021-11-30', GETDATE()),
    (5, N'The best in the series! Sirius Black is my favorite character.', 14, N'WizardingFan', '2020-08-10', GETDATE()),
    (5, N'King''s masterpiece. Pennywise will haunt your dreams forever.', 15, N'HorrorObsessed', '2021-10-25', GETDATE()),
    (4, N'Christie at her best! The twist ending is exceptional.', 16, N'MysteryAddict', '2022-02-05', GETDATE()),
    (5, N'Re-read this after 20 years and it''s still perfect!', 1, N'RetroReader', '2022-04-10', GETDATE()),
    (3, N'A bit slow at times but the ending makes up for it.', 2, N'CasualReader', '2021-12-05', GETDATE()),
    (5, N'Christie''s most iconic work for good reason.', 3, N'GoldenAgeFan', '2022-03-01', GETDATE()),
    (4, N'Not perfect but launched an amazing series.', 4, N'WesterosLover', '2021-08-12', GETDATE()),
    (5, N'A perfect romance novel that stands the test of time.', 5, N'RomanceReader', '2022-01-22', GETDATE());

-- =========================
-- 6. VERIFY DATA
-- =========================
SELECT COUNT(*) AS TotalAuthors FROM Authors;
SELECT COUNT(*) AS TotalBooks FROM Books;
SELECT COUNT(*) AS TotalReviews FROM Reviews;

-- =========================
-- 7. USEFUL QUERIES
-- =========================
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