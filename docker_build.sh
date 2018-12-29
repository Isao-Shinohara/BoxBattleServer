TMP_DIR="tmp"
TARGET_DIR="Interfaces"

if [ "$1" = "" ]; then
    echo "require augument. add build image version. e.g.)`basename $0` 1"
    exit
fi

if [ ! -d $TMP_DIR ]; then
  mkdir $TMP_DIR
fi

if [ ! -d $TMP_DIR/$TARGET_DIR ]; then
  mkdir $TMP_DIR/$TARGET_DIR
fi

cp -frp ../BoxBattleClient/Assets/Scripts/$TARGET_DIR/*.cs ./$TMP_DIR/$TARGET_DIR

docker build -t isaoshinohara/boxbattleserver:$1 .

rm -fr $TMP_DIR