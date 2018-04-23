#!/usr/bin/env sh

set -euo pipefail

scale=100
half_scale=50
edge_width=10
chevron11=-50
chevron12=0
chevron13=50
chevron14=100
chevron21=-25
chevron22=25
chevron23=75
chevron24=125
chevron_top=-5
chevron_bot=105
size=${scale}x${scale}

white="ffffff"
deep_bronze="544307"

trap 'rm -f left-mask.png right-mask.png' EXIT HUP INT QUIT PIPE TERM
convert -size "${size}" xc:black -fill white \
  -draw "polygon 0,0 0,${scale} ${scale},0" left-mask.png
convert left-mask.png -flop right-mask.png

function square {
  colour=$1
  name=$2
  convert -size "${size}" xc:"#${colour}" "${name}-square.png"
}

function circle {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -fill "#${colour}" \
    -draw "circle ${half_scale},${half_scale},${half_scale},0" \
    "${name}-circle.png"
}

function left_half_circle {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -fill "#${colour}" \
    -draw "circle 0,${half_scale},${half_scale},${half_scale}" \
    "${name}-left-half-circle.png"
}

function right_half_circle {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -fill "#${colour}" \
    -draw "circle ${scale},${half_scale},${half_scale},${half_scale}" \
    "${name}-right-half-circle.png"
}

function stick {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" \
    -strokewidth "${edge_width}"  \
    -draw "line ${half_scale},0 ${half_scale},${scale}" \
    "${name}-stick.png"
}

function passage {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" -fill none \
    -strokewidth "${edge_width}"  \
    -draw "polyline ${chevron11},${chevron_top} ${chevron12},${half_scale} ${chevron12},${half_scale} ${chevron11},${chevron_bot}" \
    -draw "polyline ${chevron12},${chevron_top} ${chevron13},${half_scale} ${chevron13},${half_scale} ${chevron12},${chevron_bot}" \
    -draw "polyline ${chevron13},${chevron_top} ${chevron14},${half_scale} ${chevron14},${half_scale} ${chevron13},${chevron_bot}" \
    "${name}-passage1.png"
  convert -size "${size}" xc:none -stroke "#${colour}" -fill none \
    -strokewidth "${edge_width}"  \
    -draw "polyline ${chevron21},${chevron_top} ${chevron22},${half_scale} ${chevron22},${half_scale} ${chevron21},${chevron_bot}" \
    -draw "polyline ${chevron22},${chevron_top} ${chevron23},${half_scale} ${chevron23},${half_scale} ${chevron22},${chevron_bot}" \
    -draw "polyline ${chevron23},${chevron_top} ${chevron24},${half_scale} ${chevron24},${half_scale} ${chevron23},${chevron_bot}" \
    "${name}-passage2.png"
    convert "${name}-passage1.png" "${name}-passage2.png" +append "${name}-passage.png"
}

function edge_left {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" \
    -strokewidth "${edge_width}"  \
    -draw "line 0,0 0,${scale}" \
    "${name}-edge-left.png"
}

function edge_right {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" \
    -strokewidth "${edge_width}"  \
    -draw "line ${scale},0 ${scale},${scale}" \
    "${name}-edge-right.png"
}

function edge_top {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" \
    -strokewidth "${edge_width}"  \
    -draw "line 0,0 ${scale},0" \
    "${name}-edge-top.png"
}

function edge_bottom {
  colour=$1
  name=$2
  convert -size "${size}" xc:none -stroke "#${colour}" \
    -strokewidth "${edge_width}"  \
    -draw "line 0,${scale} ${scale},${scale}" \
    "${name}-edge-bottom.png"
}

function edge_top_left {
  colour=$1
  name=$2
  composite -gravity center \
    "${name}-edge-top.png" \
    "${name}-edge-left.png" \
    "${name}-edge-top-left.png"
}

function edge_top_right {
  colour=$1
  name=$2
  composite -gravity center \
    "${name}-edge-top.png" \
    "${name}-edge-right.png" \
    "${name}-edge-top-right.png"
}

function upper_left {
  colour=$1
  name=$2
  convert "${name}-square.png" left-mask.png -alpha off \
    -compose copy_opacity -composite "${name}-upper-left.png"
}

function upper_right {
  colour=$1
  name=$2
  convert "${name}-square.png" right-mask.png -alpha off \
    -compose copy_opacity -composite "${name}-upper-right.png"
}

function lower_left {
  colour=$1
  name=$2
  convert -respect-parenthesis "${name}-square.png" \( right-mask.png -negate \) \
    -alpha off -compose copy_opacity -composite "${name}-lower-left.png"
}

function lower_right {
  colour=$1
  name=$2
  convert -respect-parenthesis "${name}-square.png" \( left-mask.png -negate \) \
    -alpha off -compose copy_opacity -composite "${name}-lower-right.png"
}

function all_triangles {
  colour=$1
  name=$2
  upper_left  "${colour}" "${name}"
  upper_right "${colour}" "${name}"
  lower_left  "${colour}" "${name}"
  lower_right "${colour}" "${name}"
}

function half_circles {
  colour=$1
  name=$2
  square "${colour}" "${name}"
  left_half_circle "${colour}" "${name}"
  right_half_circle "${colour}" "${name}"
}

function some_shapes {
  colour=$1
  name=$2
  square        "${colour}" "${name}"
  all_triangles "${colour}" "${name}"
}

function edges {
  colour=$1
  name=$2

  edge_left     "${colour}" "${name}"
  edge_right    "${colour}" "${name}"
  edge_top      "${colour}" "${name}"
  edge_bottom   "${colour}" "${name}"

  edge_top_left       "${colour}" "${name}"
  edge_top_right      "${colour}" "${name}"
}

some_shapes "ffffff" "white"
some_shapes "7d4439" "ironstone"
some_shapes "74b4bb" "neptune"
some_shapes "67a63e" "green"

circle "8148a8" "purple"
stick "d9e3a2" "yellow"

half_circles "${white}" "white"

square "${deep_bronze}" "deep-bronze"
half_circles "${deep_bronze}" "deep-bronze"

some_shapes "844234" "sanguine-brown"
some_shapes "555555" "emperor"
some_shapes "8449a8" "studio"

passage "68934d" "aspargus"
edges "68934d" "aspargus"
square "acb65d" "olive-green"
square "686769" "salt-box"
square "3b2f94" "minsk"
square "505251" "nandor"

tacha="ccd770"

square "${tacha}" "tacha"
upper_right "${tacha}" "tacha"
