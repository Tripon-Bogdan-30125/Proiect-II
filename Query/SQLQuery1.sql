SELECT teams.name, teams.teamValue, teams.squadSize, teams.stadium, teams.city FROM teams
inner join league on teams.idLeague = league.id 