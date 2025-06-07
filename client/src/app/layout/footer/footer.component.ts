import { Component } from '@angular/core';
import { footerBottomText, footerData, FooterSection } from './footer-data';

@Component({
  selector: 'app-footer',
  standalone: false,
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {
  footerSections: FooterSection[] = footerData;
  footerBottom: string = footerBottomText;
}
