CREATE TABLE Likes (
	UserID INTEGER REFERENCES Users(id) NOT NULL,
	PostID INTEGER REFERENCES Posts(id) NOT NULL,
	PRIMARY KEY(UserID, PostID)
);