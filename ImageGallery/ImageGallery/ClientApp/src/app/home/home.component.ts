import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit  } from '@angular/core';
import 'hammerjs';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation, NgxGalleryAction } from 'ngx-gallery';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [DatePipe]
})
export class HomeComponent {
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  galleryAction: NgxGalleryAction[];
  AllDate: any;
  AllImagesData: any;

  constructor(private http: HttpClient, public datepipe: DatePipe) {
    this.AllDate = [];
    // Getting the date from text file using server side implementations
    http.get<any>(document.getElementsByTagName('base')[0].href + 'api/Actions/GetAllDates').subscribe(result => {
      this.AllDate = result;
    }, error => console.error(error));
  }
  GetImages(val) {
    let images;
    val = this.datepipe.transform(val, 'yyyy-M-d');
    this.http.post<any>(document.getElementsByTagName('base')[0].href + 'api/Actions/GetImages', { "Date": val }).subscribe(result => {
      debugger
      images = result;
      this.galleryOptions = [];
      this.galleryImages = [];
      this.galleryAction = [];

      this.handleClick = this.handleClick.bind(this);
      this.galleryAction = [{ icon: "fa fa-arrow-circle-down", onClick: this.handleClick }];
      
      // setting up gallary on change of selection
      this.galleryOptions = [
        {
          width: '1109px',
          height: '720px',
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          previewDownload: false,
          actions: this.galleryAction
        },
        // max-width 800
        {
          breakpoint: 800,
          width: '100%',
          height: '600px',
          imagePercent: 80,
          thumbnailsPercent: 20,
          thumbnailsMargin: 20,
          thumbnailMargin: 20
        },
        // max-width 400
        {
          breakpoint: 400,
          preview: false
        },
      ];
      this.AllImagesData = [];
      for (var i = 0; i < images.photos.length; i++) {
        this.AllImagesData.push({
          small: images.photos[i].img_src,
          medium: images.photos[i].img_src,
          big: images.photos[i].img_src,
        })
      }
      this.galleryImages = this.AllImagesData;

      ;
    }, error => console.error(error));
  }
  handleClick(event, i) {
    debugger
    var src = this.AllImagesData[i].small;
    this.http.post<any>(document.getElementsByTagName('base')[0].href + 'api/Actions/Download', { "Src": src }).subscribe(result => {
      if (result)
        alert('Image saved successfully');
    }, error => console.error(error));
  }

  //downloadImage(img) {
  //  const imgUrl = img.src;
  //  const imgName = imgUrl.substr(imgUrl.lastIndexOf('/') + 1);
  //  this.httpClient.get(imgUrl, { responseType: 'blob' as 'json' })
  //    .subscribe((res: any) => {
  //      const file = new Blob([res], { type: res.type });

  //      // IEif (window.navigator && window.navigator.msSaveOrOpenBlob) {
  //      window.navigator.msSaveOrOpenBlob(file);
  //      return;
  //    }

  //         const blob = window.URL.createObjectURL(file);
  //  const link = document.createElement('a');
  //  link.href = blob;
  //  link.download = imgName;

  //  // Version link.click() to work at firefox
  //  link.dispatchEvent(new MouseEvent('click', {
  //    bubbles: true,
  //    cancelable: true,
  //    view: window
  //  }));

  //  setTimeout(() => { // firefoxwindow.URL.revokeObjectURL(blob);
  //    link.remove();
  //  }, 100);
  //});
  ngOnInit(): void {
  }
}
