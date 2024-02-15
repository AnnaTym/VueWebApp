CREATE TABLE IF NOT EXISTS Agent
(
    Id uuid NOT NULL,
    Name text NOT NULL,
    State text NOT NULL,
    Skills uuid[]
);