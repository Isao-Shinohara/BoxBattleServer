# BoxBattleServer

## Run by dotnet-cli
```
dotnet run --project BoxBattleServer > /tmp/BoxBattleServer.log 2>&1 &
```

## Build container image with docker
```
docker login
docker build -t isaoshinohara/boxbattleserver:1 .
docker push isaoshinohara/boxbattleserver:1
```

## Deploy application to Kubernetes cluster
```
kubectl apply -f kubernetes/development/
```
