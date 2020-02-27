declare @destination as table(
     idParent int,
     Nom NVARCHAR (100), 
     Niveau TINYINT, 
     Description NVARCHAR (1500) 
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
(null, 'France', 2, 'La France, pays de l''Europe occidentale, compte des villes médiévales, des villages alpins et des plages. Paris, sa capitale, est célèbre pour ses maisons de mode, ses musées d''art classique, dont celui du Louvre, et ses monuments comme la Tour Eiffel. Le pays est également réputé pour ses vins et sa cuisine raffinée. Les peintures rupestres des grottes de Lascaux, le théâtre romain de Lyon et l''immense château de Versailles témoignent de sa riche histoire.'),
(1, 'Guadeloupe', 3,'Dans un site exceptionnel, en bordure d''un petit lagon turquoise, tout est réuni pour un séjour paradisiaque. Découvrez les merveilles de grande terre et de basse terre, les joies des plongées dans la réserve naturelle.'),
(1, 'Saint-Barthélémy',3,'Imaginez une île où il fait 26 à 28 °C toute l''année. Baignez vous dans une eau turquoise.'),
(null, 'Birmanie', 2,'La Birmanie est un pays passionnant pour tous ceux qui s’intéressent à l''art, aux civilisations, à l''hindouisme. Ce pays s''ouvre et a conservé toute la richesse de son patrimoine culturel. Visitez les temples, les marchés, ...'),
(null, 'Canada', 2,'Découvrez la nature généreuse et les grandes villes du Canada en toute saison, grâce aux nombreux circuits que nous avons élaborés.'),
(1, 'Bretagne',3,'Superbe région. Terre de légendes. De nombreux spots pour le surf et le kitesurf.'),
(null, 'Australie', 2,'L''Australie est un pays et continent entouré par les océans Indien et Pacifique. Ses principales villes, Sydney, Brisbane, Melbourne, Perth et Adélaïde, sont bâties sur la côte. La capitale, Canberra, est quant à elle située à l''intérieur des terres. Le pays est connu pour l''opéra de Sydney, la Grande Barrière de corail, ses grandes étendues sauvages et désertiques appelées l''Outback, et ses espèces animales uniques comme le kangourou et l''ornithorynque.'),
(null, 'Japon', 2,'Le Japon est un pays insulaire situé dans l''océan Pacifique. Il comporte des villes denses, des palais impériaux, des parcs nationaux montagneux ainsi que des milliers de temples et de sanctuaires. Les trains à grande vitesse Shinkansen relient les îles principales de Kyūshū (avec les plages subtropicales d''Okinawa), Honshū (où se trouvent Tokyo et le mémorial en hommage aux victimes du bombardement atomique d''Hiroshima) et Hokkaidō (prisée pour le ski). Tokyo, la capitale, est réputée pour ses gratte-ciel, ses magasins et sa culture populaire.'),
(null, 'Vietnam', 2,'Le Vietnam est un pays d''Asie du Sud-Est sur la mer de Chine méridionale. Il est connu pour ses plages, ses cours d''eau, ses pagodes bouddhistes et ses villes animées. Hanoï, la capitale, rend hommage à Hô Chi Minh, l''emblématique dirigeant de la nation à l''ère communiste, avec un immense mausolée en marbre. Hô-Chi-Minh-Ville (anciennement Saigon) possède des sites d''intérêt de l''époque coloniale française, ainsi que des musées d''histoire sur la guerre du Vietnam et les tunnels de Củ Chi, utilisés par les soldats Viêt-cong.')




insert @voyage values
(2,GETDATE(),DATEADD(day,12,GETDATE()),4,100,0.20,'La résidence Caraïbes Royal privilégie concept de « vacances à la maison » offre une totale indépendance tout en vous apportant des services hôteliers attentionnés et sur-mesure, pour des vacances en famille inoubliables.'),
(6,DATEADD(day,10,GETDATE()),DATEADD(day,25,GETDATE()),5,300,0.02,'Trégastel Club Le Castel Sainte Anne sur la côte de Granit Rose, riche en patrimoine naturel et culturel avec ses 9 plages de sable fin à proximité dont une à 100 m en accès direct. Le charme du Castel (ancien couvent de 3 étages) et des Penntis (petites maisons bretonnes). À moins d''une dizaine de kilomètres de Perros-Guirec.'),
(4,GETDATE(),DATEADD(day,14,GETDATE()),8,1000,0.50,'Le lac Inle, site emblématique de Birmanie vous embarque pour quelques jours d’intense découverte. Situé dans l’état de Shan à 900 mètres d’altitude, c’est le deuxième plus grand lac du pays ! Vous y apprécierez notamment la vue sur ses montagnes environnantes, son marché traditionnel de 5 jours, ses jardins flottants et ses maisons sur pilotis.'),
(4,DATEADD(day,150,GETDATE()),DATEADD(day,250,GETDATE()),2,354,0.70,'Nous avons pensé à tout pour que ce séjour en Birmanie soit réussi et surtout qu’il plaise au plus grand nombre : une balade en vélo pour rejoindre les vignobles de Red Mountain, une visite du village d‘Indein et de ses pagodes dorées, un soupçon de marche vers Thalae U et une sortie en bateau.'),
(6,GETDATE(),DATEADD(day,25,GETDATE()),3,236,0.00,'Nous vous donnons rendez-vous à la résidence Port du Crouesty. Piétonnière et située à deux pas du Port du Crouesty, elle se compose de 3 quartiers et de bâtisses en granit à pans de bois, typique de l''architecture bretonne. Ses appartements lumineux à la décoration épurée teintée d''éléments bleus rappellent la mer. Certains offrent une vue splendide sur le port, la mer ou le jardin dont vous ne vous lasserez pas !'),
(3,DATEADD(day,50,GETDATE()),DATEADD(day,57,GETDATE()),10,750,0.20, 'Votre cottage au style caribéen, le jardin tropical, la piscine, et le panorama sur la baie de Saint-Jean. Mini Cooper cabriolet pour rayonner cheveux au vent, croisière privée en catamaran : c’est prévu !'),
(3,DATEADD(day,15,GETDATE()),DATEADD(day,30,GETDATE()),25,500,0.10, 'Goûter au paradis tropical en deux temps et deux adresses exclusives, sur les hauteurs de la baie des Flamands puis sur l''Anse Toiny. L’esprit "planteur chic", les tables créatives, le jardin tropical puis votre piscine privée'),
(3,DATEADD(day,12,GETDATE()),DATEADD(day,30,GETDATE()),8,1200,0.20, 'Eco-resort chic et engagé en bord de plage, votre hôtel confidentiel d''influence créole. Les deux visages de l’île : l''icône glamour des Caraïbes et la réserve naturelle d’exception. Un massage au spa de l’hôtel, une sortie en mer au coucher du soleil avec champagne : c''est prévu pour vous'),
(5,DATEADD(day,26,GETDATE()),DATEADD(day,36,GETDATE()),12,1530,0.40, 'Un Nouvel An inoubliable au Canada. Un séjour très spécial pour fêter votre nouvelle année sur le nouveau continent. Nous vous offrons avec ce magnifique forfait une semaine de dépaysement complet. Vous serez logé dans un lieu unique au bord du magnifique lac Taureau à l''auberge du même non.'),
(5,DATEADD(day,80,GETDATE()),DATEADD(day,100,GETDATE()),34,1990,0.50, 'Raid motoneige au Canada de 800 km. Ce séjour itinérant en motoneige au Canada est destiné aux amateurs d''aventure et de sensations nouvelles. Il s’agit d’une randonnée en motoneige de 5,5 jours qui vous fera découvrir les grands espaces blancs sur plus de 800 km. Ne vous méprenez pas, ceci n''est pas un raid extrême, vous dormirez dans des auberges très réputées, d''un grand confort, qui comptent parmi les plus prestigieuses du Québec. Les distances à parcourir chaque jour sont très raisonnables. Les journées varieront de 60 km à 180 km. Ce séjour est réalisable en DUO ou en SOLO indifféremment.'),
(5,DATEADD(day,24,GETDATE()),DATEADD(day,35,GETDATE()),5,750,0.10, 'Un de nos raids motoneige les plus populaires. Une aventure unique en motoneige au pays des trappeurs et des coureurs des bois au cœur d’une nature encore vierge et magnifique, vous découvrirez les pistes des loups, des renards et des lynx suivant celles des chevreuils.'),
(5,DATEADD(day,10,GETDATE()),DATEADD(day,30,GETDATE()),2,1100,0.24, 'Un Noël inoubliable au Canada. C’est le forfait vacances hiver fait sur mesure pour la découverte de l’essentiel des activités hivernales au Québec. Parfait pour les couples et les familles, cette semaine au Canada sera pour vous un pur bonheur de découverte et d’enchantement. Comme tous les autres séjours, vous ne serez pas dans la cohue des vacances de masses des gros voyagistes.'),
(6,DATEADD(day,100,GETDATE()),DATEADD(day,114,GETDATE()),50,250,0.10, 'Découvrir la Cornouaille. Vous ressourcer au bord de la mer. Profiter de vos proches. Voici le type de vacances que nous vous proposons à la résidence Cap Marine. Implantée sur le front de mer, cette charmante résidence de style breton vous invite à séjourner dans ses locations lumineuses. Respectueuse de l''environnement, elle est labellisée " Clef Verte " et s''intègre harmonieusement au cadre naturel qui l''entoure.'),
(6,DATEADD(day,14,GETDATE()),DATEADD(day,21,GETDATE()),8,99,0.50, 'Respirez, tout commence ici, au bord de la mer, à la résidence La Corniche de la Plage. L''air de l''Atlantique est tellement ressourçant ! Construite juste en face de la plage du Trez, cette charmante résidence vous offre une vue exceptionnelle sur la mer. Vous découvrirez des appartements cosy et intimistes dans lesquels vous pourrez vous reposer pleinement. Ouverts sur l''extérieur par un balcon, ils laissent entrer la lumière pour faire de votre intérieur un nid douillet où se retrouver en famille. Certains disposent même d''une vue sur la mer !'),
(3,DATEADD(day,20,GETDATE()),DATEADD(day,32,GETDATE()),4,799,0.30, 'Surplombant la magnifique plage des Flamands, sur les hauteurs de Colombier, Villa Marie Saint-Barth est situé au coeur d''un grand et beau jardin tropical, créant une atmosphère intimiste, typique de l''esprit « plantation ». L''hôtel invite à vivre des instants de pur plaisir. La piscine extérieure, nichée dans les jardins, permet de se rafraîchir en profitant de l''explosion de couleurs des fleurs et plantes exotiques. Le Spa Pure Altitude, avec ses soins Signature, offre des moments de pure détente. Quant au restaurant François Plantation, il propose une cuisine de tradition aux saveurs locales pour le plus grand plaisir des papilles.'),
(8,DATEADD(day,28,GETDATE()),DATEADD(day,32,GETDATE()),4,799,0.30, 'Avec ses cerisiers en fleurs et ses maisons traditionnelles en bois, votre séjour tout compris au Japon vous entraîne à la découverte de paysages sophistiqués. Prenez le départ du circuit « Les voies du Tokaïdo » et découvrez au loin le symbole du Japon : le mont Fuji et son cône blanc qui culmine à 3 776m. Plus au sud, c’est à Kyoto qu’une autre promenade zen vous attend. La bambouseraie d''Arashiyama offre de ceux dignes des décors du film « Tigre et dragon ».'),
(8,DATEADD(day,51,GETDATE()),DATEADD(day,58,GETDATE()),4,799,0.30, 'Venez vous recueillir à Kyoto, dans le sanctuaire Fushimi Inari taisha, qui doit en partie sa renommée aux milliers de torii (portails traditionnels) qui guident les fidèles sur plusieurs kilomètres. Les sanctuaires et temples de Nikko sont aussi sacrés. Inscrit au patrimoine mondial de l’UNESCO, la spiritualité et la nature s’enchevêtrent dans ce complexe religieux, niché au cœur de la forêt.'),
(9,DATEADD(day,5,GETDATE()),DATEADD(day,12,GETDATE()),4,799,0.30, 'Un parcours dense et contrasté au Vietnam, du Nord au Sud : ses paysages mythiques, de la baie d''Halong aux villes historiques du Centre, jusqu''à la vie presque amphibie du delta du Mékong où se mêlent vergers et cocoteraies ; ses prestigieux témoins d''un riche passé, des temples confucéens du Nord aux étranges églises coloniales du Sud ; ses capitales au charme indicible où s''expose le nouveau visage d''un pays en pleine mutation.')





insert @photos values
('guadeloupe_1.jpg', 2),
('guadeloupe_2.jpg', 2),
('birmanie_1.jpg', 4),
('birmanie_2.jpg', 4),
('birmanie_3.jpg', 4),
('bretagne_1.jpg', 6),
('canada_1.jpg', 5),
('canada_2.jpg', 5),
('saint-barth_1.jpg', 3),
('saint-barth_1.jpg', 3),
('france_1.jpg', 1),
('japon_1.jpg', 8),
('japon_2.jpg', 8),
('vietnam_1.jpg', 9),
('vietnam_2.png', 9)




insert Destination select * from @destination

insert Voyage select * from @voyage

insert Photo select * from @photos

