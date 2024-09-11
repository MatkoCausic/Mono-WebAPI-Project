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
	
INSERT INTO "Aquarium" ("OwnerName","Shape","IsHandmade","Volume")
VALUES
	('Klara','Bowl',false,90.5);
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

SELECT a."Id" as "AquariumId","OwnerName","Shape","IsHandmade","Volume",f."Id" as "FishId","Name","Color","IsAggressive","AquariumId"
FROM "Aquarium" a
LEFT JOIN "Fish" f
ON a."Id" = '';

SELECT a."Id" as "AquariumId","OwnerName","Shape","IsHandmade","Volume",
                    f."Id" as "FishId","Name","Color","IsAggressive","AquariumId"
                    FROM "Aquarium" a
                    LEFT JOIN "Fish" f
					ON a."Id" = f."AquariumId"
                    WHERE a."Id" = '6b1502e7-fc2e-4a1c-89b6-0c37f79f0021';

SELECT * FROM "Aquarium" WHERE "Id" = '6b1502e7-fc2e-4a1c-89b6-0c37f79f0021';
SELECT * FROM "Aquarium" WHERE "Id" = 'fd39f1c1-ae2c-4a05-a0aa-4a044263780b';