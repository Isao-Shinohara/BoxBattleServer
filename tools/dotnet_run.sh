#!/bin/sh
if [ "$1" = "" ]; then
    echo "require environment augument. e.g.)`basename $0` Development"
    exit
fi

dotnet run --launch-profile $1 --project BoxBattleServer &