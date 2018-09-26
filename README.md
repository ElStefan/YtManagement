# YtManagement
Goes through all your youtube subscriptions and moves videos to playlists according to your own rules
Features:
- ASP.NET MVC Service
- Using .NET Core 2.0 for linux compatibility
- Available as docker container on elstefan/ytmanagement: `docker pull elstefan/ytmanagement:latest`

Requirements:
- Requires a Google Developer Account and access to Youtube Data API v3 (50. Mio requests/day for free)
- Docker
  - Expects a working folder on path `/data`
  - Expects the file `/data/client_id.json` which should contain the client_id and client_secret from your Google Dev Account to access the Youtube API
