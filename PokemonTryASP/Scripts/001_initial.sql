CREATE TABLE IF NOT EXISTS "Companies"(
    "Id" serial PRIMARY KEY,
    "Name" text NOT NULL
);

CREATE TABLE IF NOT EXISTS "Departments"(
    "Id" serial PRIMARY KEY,
    "Name" text NOT NULL,
    "Phone" text NOT NULL,
    "CompanyId" int REFERENCES "Companies"("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "Workers"(
     "Id" serial NOT NULL PRIMARY KEY,
     "Name" text NOT NULL,
     "Surname" text NOT NULL,
     "Phone" text NOT NULL,
     "CompanyId" int REFERENCES "Companies"("Id") ON DELETE CASCADE,
     "DepartmentId" int REFERENCES "Departments"("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "Passports"(
    "Id" serial PRIMARY KEY,
    "Type" text NOT NULL,
    "Number" text NOT NULL UNIQUE,
    "WorkerId" int REFERENCES "Workers"("Id") ON DELETE CASCADE
);
