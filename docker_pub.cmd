docker tag pad/webaggregator:linux-latest danieladoni01/webaggregator
docker tag pad/currency1.api:linux-latest danieladoni01/currency1.api
docker tag pad/currency2.api:linux-latest danieladoni01/currency2.api
docker tag pad/currency3.api:linux-latest danieladoni01/currency3.api
docker tag pad/football1.api:linux-latest danieladoni01/football1.api
docker tag pad/football2.api:linux-latest danieladoni01/football2.api
docker tag pad/football3.api:linux-latest danieladoni01/football3.api
docker login
docker push danieladoni01/webaggregator
docker push danieladoni01/currency1.api
docker push danieladoni01/currency2.api
docker push danieladoni01/currency3.api
docker push danieladoni01/football1.api
docker push danieladoni01/football2.api
docker push danieladoni01/football3.api