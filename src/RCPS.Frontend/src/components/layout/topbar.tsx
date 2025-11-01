import { Menu } from "lucide-react";
import { Sheet, SheetContent, SheetTrigger } from "../ui/sheet";
import { Sidebar } from "./sidebar";
import { Button } from "../ui/button";

export function Topbar() {
  return (
    <header className="flex h-14 items-center justify-between border-b bg-background px-4 md:hidden">
      <Sheet>
        <SheetTrigger asChild>
          <Button variant="ghost" size="icon">
            <Menu className="h-5 w-5" />
          </Button>
        </SheetTrigger>
        <SheetContent side="left" className="p-0">
          <Sidebar />
        </SheetContent>
      </Sheet>
      <span className="text-sm font-semibold">RCPS Implementation System</span>
    </header>
  );
}
