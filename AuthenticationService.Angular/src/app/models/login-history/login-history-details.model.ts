export interface LoginHistoryDetailsModel {
  id: number;
  userId: number | null;
  username: string | null;
  name: string | null;
  status: boolean;
  failureReason: string | null;
  ipAddress: string | null;
  device: string | null;
  browser: string | null;
  dateTime: Date;
}
