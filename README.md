# Art Of Rally Multiplayer Mod

[![](https://img.shields.io/github/v/release/Theaninova/aormp?label=Download)](https://github.com/Theaninova/aormp/releases/latest)
![](https://img.shields.io/badge/Game%20Version-v1.3.3a-blue)
[![GitHub license](https://img.shields.io/github/license/theaninova/aormp.svg)](https://github.com/wulkanat/aormp/blob/master/LICENSE)

[![](https://img.shields.io/badge/Server-GitHub-23292F)](https://github.com/Theaninova/aorc-server)

A mod that allows for real-time streaming of data via websockets to a
server which forwards them to one or more observers.

#### Discord
[![Art Or Rally Discord](https://badgen.net/discord/members/Sx3e7qGTh9)](https://discord.gg/Sx3e7qGTh9)

#### Launcher Support
![](https://img.shields.io/badge/GOG-Supprted-green)
![](https://img.shields.io/badge/Steam-Supprted-green)
![](https://img.shields.io/badge/Epic-Untested-yellow)

#### Platform Support
![](https://img.shields.io/badge/Windows-Supprted-green)
![](https://img.shields.io/badge/Linux-Untested-yellow)
![](https://img.shields.io/badge/OS%2FX-Untested-yellow)
![](https://img.shields.io/badge/PlayStation-Not%20Supprted-red)
![](https://img.shields.io/badge/XBox-Not%20Supprted-red)
![](https://img.shields.io/badge/Switch-Not%20Supprted-red)

## Usage

[![](https://img.shields.io/github/v/release/Theaninova/aromp?label=Download%20Client%20Mod)](https://github.com/Theaninova/aormp/releases/latest)
[![](https://img.shields.io/github/v/release/Theaninova/aorc-server?label=Download%20Server)](https://github.com/Theaninova/aorc-server/releases/latest)

1. Download the client and install it using the [Unity Mod Manager](https://www.nexusmods.com/site/mods/21/)
2. *optional* Download the server for self-hosting

### Change Server

Go to `[Game DIR]/Mods/MultiplayerMod/Settings.xml` and edit the
`<URL>` tag's content. DO NOT add a trailing `/` to the URL.

Default server hosted by me is `http://h2862963.stratoserver.net:4593`

## Using the mod in-game

1. Make sure multiplayer is enabled in the mod options
2. Make sure to use a custom lobby id
3. Make sure both you and your opponent appear in the list in the top
   left corner. *Note: as of now, only two players are supported.
   More than two people in the same lobby will result in the ghost
   jumping around*
4. Start the same rally (only time attack supported for now). You are
   Responsible to make sure that both of you select the same map, but
   you're free to mix and match cars & classes (even maps, but welp that's
   gonna look interesting lol)
5. Make sure you select *any* ghost. If you don't select a ghost, you
   won't see your opponent.
   *Note: As of now, player chosen cars are not synced so you will
   see whatever car the ghost would have*
6. One of you starts the event, the other player will be started up
   automatically

*Note: There is zero ping compensation measures, so your opponent will
already be further along the track than you see, depending on your ping*

## Features

TODO;

## Planned Features

TODO;