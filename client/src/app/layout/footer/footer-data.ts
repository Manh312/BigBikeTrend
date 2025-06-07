export interface FooterSection {
  title: string;
  links: { name: string; url: string }[];
}

export const footerData: FooterSection[] = [
  {
    title: 'Sản phẩm',
    links: [
      { name: 'Điện thoại', url: '#' },
      { name: 'Phụ kiện', url: '#' },
      { name: 'Máy tính bảng', url: '#' },
      { name: 'Smartwatch', url: '#' },
    ],
  },
  {
    title: 'Hỗ trợ',
    links: [
      { name: 'Trung tâm trợ giúp', url: '#' },
      { name: 'Chính sách đổi trả', url: '#' },
      { name: 'Câu hỏi thường gặp', url: '#' },
    ],
  },
  {
    title: 'Về chúng tôi',
    links: [
      { name: 'Giới thiệu', url: '#' },
      { name: 'Liên hệ', url: '#' },
      { name: 'Tuyển dụng', url: '#' },
    ],
  },
  {
    title: 'Chính sách',
    links: [
      { name: 'Điều khoản sử dụng', url: '#' },
      { name: 'Chính sách bảo mật', url: '#' },
      { name: 'Hướng dẫn mua hàng', url: '#' },
    ],
  },
];

export const footerBottomText: string = '©2025 Mạnh-Iphone. Tất cả quyền được bảo lưu.';