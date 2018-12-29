# BoxBattleServer

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
