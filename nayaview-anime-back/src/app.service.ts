import { Injectable } from '@nestjs/common';

@Injectable()
export class AppService {
  getHello(): string {
    return 'Im Nayaview Anime API Service and I am alive!';
  }
}
