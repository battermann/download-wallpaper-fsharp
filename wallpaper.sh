#!/bin/bash
path=$(fsharpi download_wallpaper.fsx)
file="file://$path"
gsettings set org.gnome.desktop.background picture-uri $file