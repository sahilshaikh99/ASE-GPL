using ASEProject;
using System.Threading;
using System.Windows.Forms;

public class ProgramRunner
{
    private DrawHandler drawHandler;
    private PictureBox canvasShape;
    private RichTextBox programWindow;

    // Semaphore to control access to the critical section
    private SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    public ProgramRunner(PictureBox canvasShape, RichTextBox programWindow)
    {
        this.canvasShape = canvasShape;
        this.programWindow = programWindow;
        this.drawHandler = new DrawHandler(canvasShape.Width, canvasShape.Height, canvasShape);
    }

    public void RunProgram(string program)
    {
        try
        {
            // Wait until the semaphore is available (no other thread is in the critical section)
            semaphore.Wait();

            // Execute the program on the separate thread
            drawHandler.ExecuteMultilineCommand(program);

            // Update the canvas image on the UI thread
            canvasShape.Invoke((MethodInvoker)delegate
            {
                canvasShape.Image = drawHandler.GetCanvasImage();
            });
        }
        finally
        {
            // Release the semaphore (allowing other threads to enter the critical section)
            semaphore.Release();
        }
    }
}