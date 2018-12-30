if [ "$1" = "" ]; then
    echo "require augument. add build image version. e.g.)`basename $0` 1"
    exit
fi

docker build -t isaoshinohara/boxbattleserver:$1 .