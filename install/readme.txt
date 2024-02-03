docker hub (https://hub.docker.com/repositories/danieladoni01)
Pentru a putea incarca si lansa containerele descarcam docker desktop (https://www.docker.com/products/docker-desktop/)

Faceti download la fisierele: docker-compose.yml docker-compose.override.yml de pe github (https://github.com/DoniDaniela/PAD_task/tree/main/install)

Lansam CMD cd la mapa unde sunt fisierele de mai sus
Lansam comenzile:
docker-compose build
docker-compose up

Pentru a vedea datele din redis cache avem nevoie de un Redis GUI
https://github.com/qishibo/AnotherRedisDesktopManager/releases
In GUI configuram accesul local la Redis
localhost@6379

Pentru a accesa logurile din prometheus in browser accesam:
http://localhost:9090/

Pentru a folosi serviciile descarcam de pe git (https://github.com/DoniDaniela/PAD_task/tree/main/install) colectia Postman 