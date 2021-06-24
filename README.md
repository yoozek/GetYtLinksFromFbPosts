# YtVideosFromFb

## Overview

Simple console application in .NET 6 using Facebook Graph API to get all posts from feed and extract links to YouTube videos

## Usage

You'll need to create Facebook App and generate User Token with user_posts permission
https://developers.facebook.com/docs/graph-api/using-graph-api/

Just paste the token to appsettings.json in AppConfig:AppToken section, build and run the app.
By default it creates text file in run directory with all extracted YouTube links.

Optional: Use https://www.labnol.org/internet/youtube-playlist-spreadsheet/29183/ to paste all links and get thumbnails in Google Spreadsheet