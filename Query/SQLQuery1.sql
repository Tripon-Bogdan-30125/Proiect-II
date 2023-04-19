ALTER TABLE staff
ADD FOREIGN KEY (idTeam)
REFERENCES nationalTeam(id);