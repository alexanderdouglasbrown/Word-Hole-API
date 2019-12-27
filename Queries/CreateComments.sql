CREATE TABLE Comments (
	ID SERIAL PRIMARY KEY,
	UserID INTEGER REFERENCES Users(id) NOT NULL,
	PostID INTEGER REFERENCES Posts(id) NOT NULL,
	Comment TEXT NOT NULL,
	CreatedOn TIMESTAMP NOT NULL DEFAULT NOW()
);