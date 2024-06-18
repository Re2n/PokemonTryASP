CREATE TABLE IF NOT EXISTS "Workers"(
    "Id" serial NOT NULL PRIMARY KEY,
    "Name" text NOT NULL,
    "Surname" text NOT NULL,
    "Phone" text NOT NULL,
    "CompanyId" integer NOT NULL,
    "Bobik" integer NOT NULL
)