export interface LoginHistory {
  id: number;
  userID?: number;
  username?: string;
  name?: string;
  status: number;
  failureReason?: string;
  ipAddress?: string;
  device?: string;
  browser?: string;
  dateTime: Date;
}
