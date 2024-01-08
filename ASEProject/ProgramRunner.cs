using ASEProject;
using System.Threading;
using System.Windows.Forms;

public class ProgramRunner
{
    private DrawHandler drawHandler;
    private PictureBox canvasShape;
    private RichTextBox programWindow;

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
            semaphore.Wait();

            drawHandler.ExecuteMultilineCommand(program);

            canvasShape.Invoke((MethodInvoker)delegate
            {
                canvasShape.Image = drawHandler.GetCanvasImage();
            });
        }
        finally
        {
            semaphore.Release();
        }
    }
}