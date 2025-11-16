import { Inject, Injectable, Logger } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { MAILGUN_CLIENT } from './mail.constants';

type MailgunMessages = {
  create: (domain: string, data: Record<string, unknown>) => Promise<unknown>;
};

type MailgunClient = {
  messages: MailgunMessages;
};

function isError(e: unknown): e is Error {
  if (e instanceof Error) return true;
  if (typeof e !== 'object' || e === null) return false;
  const maybe = e as Record<string, unknown>;
  return typeof maybe.message === 'string';
}

@Injectable()
export class MailService {
  private readonly logger = new Logger(MailService.name);
  private readonly domain: string;

  constructor(
    @Inject(MAILGUN_CLIENT) private mgClientRaw: unknown,
    private config: ConfigService,
  ) {
    this.domain = this.config.get<string>('MAILGUN_DOMAIN') || '';
  }

  private get client(): MailgunClient {
    const c = this.mgClientRaw as Partial<MailgunClient>;
    if (!c || !c.messages || typeof c.messages.create !== 'function') {
      throw new Error('Mailgun client is not correctly initialized');
    }
    return c as MailgunClient;
  }

  async sendMail(to: string, subject: string, html?: string, text?: string) {
    const data: Record<string, unknown> = {
      from: `${this.config.get('MAIL_FROM_NAME')} <${this.config.get('MAIL_FROM_EMAIL')}>`,
      to,
      subject,
    };

    if (html) data.html = html;
    if (text) data.text = text;

    try {
      const res = await this.client.messages.create(this.domain, data);
      this.logger.debug('Mailgun response: ' + JSON.stringify(res));
      return res;
    } catch (err: unknown) {
      if (isError(err)) {
        this.logger.error(`Failed to send email: ${err.message}`, err.stack);
        throw err;
      }
      const normalized = new Error(String(err));
      this.logger.error(
        `Failed to send email: ${normalized.message}`,
        normalized.stack,
      );
      throw normalized;
    }
  }
}
