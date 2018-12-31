# BoxBattleServer

## Run by dotnet-cli
```
./tools/dotnet_run_local.sh > /tmp/BoxBattleServer.log 2>&1 &
```

## Deploy application to Kubernetes cluster
```
kubectl apply -f kubernetes/development/
```

## Tools
### build docker image with image version number
```
# e.g.) ./tools/docker_build.sh 1

./tools/docker_build.sh [image number]
```
### push docker image with image version number
```
# require "docker login"
# e.g.) ./tools/docker_push.sh 1

./tools/docker_push.sh [image number]
```
### generate MagicOnionCode
```
./tools/generate_magic_onion_code.sh
```
### generate MessagePackCode
```
./tools/generate_message_pack_code.sh
```
