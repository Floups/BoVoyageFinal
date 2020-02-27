declare @destination as table(
     id int,
     Nom NVARCHAR (100) , 
     Niveau TINYINT, 
     Description NVARCHAR (500) 
)

declare @voyage as table(
     IdDestination INTEGER , 
     DateDepart DATE , 
     DateRetour DATE , 
     PlacesDispo INTEGER , 
     PrixHT DECIMAL (16,4) DEFAULT 0 , 
     Reduction DECIMAL (3,2) , 
     Descriptif Nvarchar(500)
)

declare @photos as table(
     NomFichier NVARCHAR(100),
     IdDestination INT
)

insert @destination values
(1,'Guadeloupe', 3,'Dans un site exceptionnel, en bordure d''un petit lagon turquoise, tout est réuni pour un séjour paradisiaque. Découvrez les merveilles de grande terre et de basse terre, les joies des plongées dans la réserve naturelle.'),
(2,'Saint-Barthélémy',3,'Imaginez une île où il fait 26 à 28 °C toute l''année. Baignez vous dans une eau turquoise.'),
(3,'Birmanie',2,'La Birmanie est un pays passionnant pour tous ceux qui s’intéressent à l''art, aux civilisations, à l''hindouisme. Ce pays s''ouvre et a conservé toute la richesse de son patrimoine culturel. Visitez les temples, les marchés, ...'),
(4,'Canada',2,'Découvrez la nature généreuse et les grandes villes du Canada en toute saison, grâce aux nombreux circuits que nous avons élaborés.'),
(5,'Bretagne',3,'Superbe région. Terre de légendes. De nombreux spots pour le surf et le kitesurf.')

insert @voyage values
(1,GETDATE(),DATEADD(day,10,GETDATE()),4,100,0.20,'La résidence Caraïbes Royal privilégie concept de « vacances à la maison » offre une totale indépendance tout en vous apportant des services hôteliers attentionnés et sur-mesure, pour des vacances en famille inoubliables.'),
(5,DATEADD(day,10,GETDATE()),DATEADD(day,25,GETDATE()),5,300,0.02,'pluie'),
(3,GETDATE(),DATEADD(day,10,GETDATE()),8,1000,0.50,'nuageux'),
(4,GETDATE(),DATEADD(day,10,GETDATE()),2,354,0.70,'sirop d''erable'),
(5,GETDATE(),DATEADD(day,10,GETDATE()),3,236,0.00,'cidre et crepe')

insert @photos values
('guadeloupe_1.jpg', 1),
('guadeloupe_2.jpg', 1)

insert Destination select * from @destination

insert Voyage select * from @voyage

insert Photo select * from @photos

