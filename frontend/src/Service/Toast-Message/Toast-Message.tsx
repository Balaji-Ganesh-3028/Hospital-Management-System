import { toast } from "react-toastify";
import { ToastMessageTypes } from "../../Enums/Toast-Message";

export const notify = (message: string, type: "success" | "error" | "info" | "warning") => {
  switch (type) {
    case ToastMessageTypes.SUCCESS:
      toast.success(message, { autoClose: 1500 });
      break;
    case ToastMessageTypes.ERROR:
      toast.error(message, { autoClose: 1500 });
      break;
    case ToastMessageTypes.INFO:
      toast.info(message, { autoClose: 1500 });
      break;
    case ToastMessageTypes.WARNING:
      toast.warning(message, { autoClose: 1500 });
      break;
  }
}