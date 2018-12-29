if [ "$1" = "" ]; then
    echo "require augument. add push image version. e.g.)`basename $0` 1"
    exit
fi

docker push isaoshinohara/boxbattleserver:$1