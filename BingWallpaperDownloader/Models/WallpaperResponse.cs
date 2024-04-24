﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*
 {
    "images": [
        {
            "startdate": "20240424",
            "fullstartdate": "202404240700",
            "enddate": "20240425",
            "url": "/th?id=OHR.TrilliumOntario_EN-US5180679465_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp",
            "urlbase": "/th?id=OHR.TrilliumOntario_EN-US5180679465",
            "copyright": "White trilliums blooming in Ontario, Canada (© Jun Zhang/Getty Images)",
            "copyrightlink": "https://www.bing.com/search?q=White+trillium&form=hpcapt&filters=HpDate%3a%2220240424_0700%22",
            "title": "Hey, how's it growing today?",
            "quiz": "/search?q=Bing+homepage+quiz&filters=WQOskey:%22HPQuiz_20240424_TrilliumOntario%22&FORM=HPQUIZ",
            "wp": true,
            "hsh": "2d2c49df10a1c386f159beb5bf9d674f",
            "drk": 1,
            "top": 1,
            "bot": 1,
            "hs": []
        }
    ],
    "tooltips": {
        "loading": "Loading...",
        "previous": "Previous image",
        "next": "Next image",
        "walle": "This image is not available to download as wallpaper.",
        "walls": "Download this image. Use of this image is restricted to wallpaper only."
    }
}
 */

namespace BingWallpaperDownloader.Logic {

    public class WallpaperResponse {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("images")]
        public List<Image>? Images { get; set; }

        [JsonProperty("tooltips")]
        public Tooltips? Tooltips { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}