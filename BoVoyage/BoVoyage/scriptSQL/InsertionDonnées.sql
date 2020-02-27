declare @destination as table(
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
     Descriptif Nvarchar(1500)
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
(1,GETDATE(),DATEADD(day,12,GETDATE()),4,100,0.20,'La résidence Caraïbes Royal privilégie concept de « vacances à la maison » offre une totale indépendance tout en vous apportant des services hôteliers attentionnés et sur-mesure, pour des vacances en famille inoubliables.'),
(5,DATEADD(day,10,GETDATE()),DATEADD(day,25,GETDATE()),5,300,0.02,'Trégastel Club Le Castel Sainte Anne sur la côte de Granit Rose, riche en patrimoine naturel et culturel avec ses 9 plages de sable fin à proximité dont une à 100 m en accès direct. Le charme du Castel (ancien couvent de 3 étages) et des Penntis (petites maisons bretonnes). À moins d''une dizaine de kilomètres de Perros-Guirec.'),
(3,GETDATE(),DATEADD(day,14,GETDATE()),8,1000,0.50,'Le lac Inle, site emblématique de Birmanie vous embarque pour quelques jours d’intense découverte. Situé dans l’état de Shan à 900 mètres d’altitude, c’est le deuxième plus grand lac du pays ! Vous y apprécierez notamment la vue sur ses montagnes environnantes, son marché traditionnel de 5 jours, ses jardins flottants et ses maisons sur pilotis.'),
(3,DATEADD(day,150,GETDATE()),DATEADD(day,250,GETDATE()),2,354,0.70,'Nous avons pensé à tout pour que ce séjour en Birmanie soit réussi et surtout qu’il plaise au plus grand nombre : une balade en vélo pour rejoindre les vignobles de Red Mountain, une visite du village d‘Indein et de ses pagodes dorées, un soupçon de marche vers Thalae U et une sortie en bateau.'),
(5,GETDATE(),DATEADD(day,25,GETDATE()),3,236,0.00,'Nous vous donnons rendez-vous à la résidence Port du Crouesty. Piétonnière et située à deux pas du Port du Crouesty, elle se compose de 3 quartiers et de bâtisses en granit à pans de bois, typique de l''architecture bretonne. Ses appartements lumineux à la décoration épurée teintée d''éléments bleus rappellent la mer. Certains offrent une vue splendide sur le port, la mer ou le jardin dont vous ne vous lasserez pas !'),
(2,DATEADD(day,50,GETDATE()),DATEADD(day,57,GETDATE()),10,750,0.20, 'Votre cottage au style caribéen, le jardin tropical, la piscine, et le panorama sur la baie de Saint-Jean. Mini Cooper cabriolet pour rayonner cheveux au vent, croisière privée en catamaran : c’est prévu !'),
(2,DATEADD(day,15,GETDATE()),DATEADD(day,30,GETDATE()),25,500,0.10, 'Goûter au paradis tropical en deux temps et deux adresses exclusives, sur les hauteurs de la baie des Flamands puis sur l''Anse Toiny. L’esprit "planteur chic", les tables créatives, le jardin tropical puis votre piscine privée'),
(2,DATEADD(day,12,GETDATE()),DATEADD(day,30,GETDATE()),8,1200,0.20, 'Eco-resort chic et engagé en bord de plage, votre hôtel confidentiel d''influence créole. Les deux visages de l’île : l''icône glamour des Caraïbes et la réserve naturelle d’exception. Un massage au spa de l’hôtel, une sortie en mer au coucher du soleil avec champagne : c''est prévu pour vous'),
(4,DATEADD(day,26,GETDATE()),DATEADD(day,36,GETDATE()),12,1530,0.40, 'Un Nouvel An inoubliable au Canada. Un séjour très spécial pour fêter votre nouvelle année sur le nouveau continent. Nous vous offrons avec ce magnifique forfait une semaine de dépaysement complet. Vous serez logé dans un lieu unique au bord du magnifique lac Taureau à l''auberge du même non.'),
(4,DATEADD(day,80,GETDATE()),DATEADD(day,100,GETDATE()),34,1990,0.50, 'Raid motoneige au Canada de 800 km. Ce séjour itinérant en motoneige au Canada est destiné aux amateurs d''aventure et de sensations nouvelles. Il s’agit d’une randonnée en motoneige de 5,5 jours qui vous fera découvrir les grands espaces blancs sur plus de 800 km. Ne vous méprenez pas, ceci n''est pas un raid extrême, vous dormirez dans des auberges très réputées, d''un grand confort, qui comptent parmi les plus prestigieuses du Québec. Les distances à parcourir chaque jour sont très raisonnables. Les journées varieront de 60 km à 180 km. Ce séjour est réalisable en DUO ou en SOLO indifféremment.'),
(4,DATEADD(day,24,GETDATE()),DATEADD(day,35,GETDATE()),5,750,0.10, 'Un de nos raids motoneige les plus populaires. Une aventure unique en motoneige au pays des trappeurs et des coureurs des bois au cœur d’une nature encore vierge et magnifique, vous découvrirez les pistes des loups, des renards et des lynx suivant celles des chevreuils.'),
(4,DATEADD(day,10,GETDATE()),DATEADD(day,30,GETDATE()),2,1100,0.24, 'Un Noël inoubliable au Canada. C’est le forfait vacances hiver fait sur mesure pour la découverte de l’essentiel des activités hivernales au Québec. Parfait pour les couples et les familles, cette semaine au Canada sera pour vous un pur bonheur de découverte et d’enchantement. Comme tous les autres séjours, vous ne serez pas dans la cohue des vacances de masses des gros voyagistes.'),
(5,DATEADD(day,100,GETDATE()),DATEADD(day,114,GETDATE()),50,250,0.10, 'Découvrir la Cornouaille. Vous ressourcer au bord de la mer. Profiter de vos proches. Voici le type de vacances que nous vous proposons à la résidence Cap Marine. Implantée sur le front de mer, cette charmante résidence de style breton vous invite à séjourner dans ses locations lumineuses. Respectueuse de l''environnement, elle est labellisée " Clef Verte " et s''intègre harmonieusement au cadre naturel qui l''entoure.'),
(5,DATEADD(day,14,GETDATE()),DATEADD(day,21,GETDATE()),8,99,0.50, 'Respirez, tout commence ici, au bord de la mer, à la résidence La Corniche de la Plage. L''air de l''Atlantique est tellement ressourçant ! Construite juste en face de la plage du Trez, cette charmante résidence vous offre une vue exceptionnelle sur la mer. Vous découvrirez des appartements cosy et intimistes dans lesquels vous pourrez vous reposer pleinement. Ouverts sur l''extérieur par un balcon, ils laissent entrer la lumière pour faire de votre intérieur un nid douillet où se retrouver en famille. Certains disposent même d''une vue sur la mer !'),
(2,DATEADD(day,20,GETDATE()),DATEADD(day,32,GETDATE()),4,799,0.30, 'Surplombant la magnifique plage des Flamands, sur les hauteurs de Colombier, Villa Marie Saint-Barth est situé au coeur d''un grand et beau jardin tropical, créant une atmosphère intimiste, typique de l''esprit « plantation ». L''hôtel invite à vivre des instants de pur plaisir. La piscine extérieure, nichée dans les jardins, permet de se rafraîchir en profitant de l''explosion de couleurs des fleurs et plantes exotiques. Le Spa Pure Altitude, avec ses soins Signature, offre des moments de pure détente. Quant au restaurant François Plantation, il propose une cuisine de tradition aux saveurs locales pour le plus grand plaisir des papilles.')



insert @photos values
('guadeloupe_1.jpg', 1),
('guadeloupe_2.jpg', 1),
('birmanie_1.jpg', 3),
('birmanie_2.jpg', 3),
('birmanie_3.jpg', 3),
('bretagne_1.jpg', 5),
('canada_1.jpg', 4),
('canada_2.jpg', 4),
('saint-barth_1.jpg', 2),
('saint-barth_1.jpg', 2)


insert Destination select * from @destination

insert Voyage select * from @voyage

insert Photo select * from @photos

