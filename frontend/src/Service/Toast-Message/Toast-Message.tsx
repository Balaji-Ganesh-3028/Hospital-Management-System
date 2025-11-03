import { toast } from "react-toastify";
import { ToastMessageTypes } from "../../Enums/Toast-Message";

export const notify = (message: string, type: "success" | "error" | "info" | "warning") => {
  switch (type) {
    case ToastMessageTypes.SUCCESS:
      toast.success(message);
      break;
    case ToastMessageTypes.ERROR:
      toast.error(message);
      break;
    case ToastMessageTypes.INFO:
      toast.info(message);
      break;
    case ToastMessageTypes.WARNING:
      toast.warning(message);
      break;
  }
}