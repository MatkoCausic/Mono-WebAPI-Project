DROP TABLE IF EXISTS "Fish";
DROP TABLE IF EXISTS "Aquarium";

CREATE TABLE "Aquarium"(
"Id" uuid DEFAULT gen_random_uuid() PRIMARY KEY,
"OwnerName" VARCHAR,
"Shape" VARCHAR,
"IsHandmade" BOOL NOT NULL,
"Volume" DECIMAL(4,2)
);
CREATE TABLE "Fish"(
"Id" uuid DEFAULT gen_random_uuid() PRIMARY KEY,
"Name" VARCHAR,
"Color" VARCHAR NOT NULL,
"IsAggressive" BOOL,
"AquariumId" uuid,
CONSTRAINT "FK_Fish_Aquarium_AquariumId"
	FOREIGN KEY("AquariumId")
		REFERENCES "Aquarium"("Id")
);

INSERT INTO "Aquarium" ("OwnerName","Shape","IsHandmade","Volume")
VALUES
	('Matko','Block',true,10);
SELECT * FROM "Aquarium";

INSERT INTO "Fish" ("Name","Color","IsAggressive","AquariumId")
VALUES
	('Leonardo','Blue',false,(SELECT "Id" FROM "Aquarium" WHERE "OwnerName" = 'Matko')),
	('Michelangelo','Orange',false,(SELECT "Id" FROM "Aquarium" WHERE "OwnerName" = 'Matko')),
	('Donatello','Purple',false,(SELECT "Id" FROM "Aquarium" WHERE "OwnerName" = 'Matko')),
	('Raphael','Red',true,(SELECT "Id" FROM "Aquarium" WHERE "OwnerName" = 'Matko'));
SELECT * FROM "Fish";

INSERT INTO "Fish" ("Name","Color","IsAggressive","AquariumId")
VALUES	('Test','White',true,null);

DELETE FROM "Fish" WHERE "Id" = '6d886490-51e4-429c-ac5b-ba80341852d0';
DELETE FROM "Fish" WHERE "Name" = 'Test';

UPDATE "Fish"
SET "IsAggressive" = TRUE
WHERE "Name" = 'Leonardo';

SELECT * FROM "Fish" WHERE "Name" = 'Raphael';

DELETE FROM "Fish"
WHERE "Name" = 'Test' OR "Name" = 'Bruno';