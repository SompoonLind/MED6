import subprocess
import os

# Install required packages
#subprocess.call(['pip', 'install', '-r', 'requirements.txt'])

import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np
import os
import cv2
import sys

# Get the first command-line argument as the input string
input_string = sys.argv[1]
#input_string = input_string[1:-1]

# Use the input string in your Python code
print(f"Raw input string: {input_string}")
print(f"Formatted input string: {f'{input_string}'}")
print(f"Is input string a string? {isinstance(input_string, str)}")
try:
    script_dir = os.path.dirname(os.path.abspath(__file__))

    folder_path = 'tmpFiles'
    isExist = os.path.exists(folder_path)
    if not isExist:
        os.makedirs(folder_path)

except Exception as e:
    print(f"An error occurred while deleting old images: {e}")


class Scrubber():
    def __init__(self):
        df = pd.read_csv(input_string, delimiter=";", decimal=",")  # Read the data from csv file
        Px = df['X'].round(1)  # Round X to 1 decimal place
        Py = df['Z'].round(1)  # Round Z to 1 decimal place
        Positions = pd.concat([Px, Py], axis=1)  # Combine Px and Py into a DataFrame
        unique_rows = Positions.drop_duplicates()  # Remove duplicate rows
        counts = Positions.groupby(list(unique_rows.columns)).size().reset_index(name='Count')  # Count the number of occurrences for each unique row
        SaveFile = pd.merge(unique_rows, counts, on=list(unique_rows.columns))  # Merge unique rows with their corresponding count values
        header = "X;Z;Count"
        SaveFile.to_csv('TesterCleaned.csv', sep=';', header=header, index=False, float_format='%.1f')  # Save the DataFrame to a CSV file
        print(SaveFile)

print("made it here")
try:
    Scrubber = Scrubber()
except Exception as e:
    print(f"An error occurred while running Scrubber: {e}")
print("hej")


def ImageMaker():
    i = 1

    df = pd.read_csv('TesterCleaned.csv', sep=";", header=0)

   # Get the min and max values for X and Z from the entire dataset
    x_min = df['X'].min()
    x_max = df['X'].max()
    z_min = df['Z'].min()
    z_max = df['Z'].max()
    num_rows, num_cols = df.shape

    # Define the size of the figure in inches
    fig_size = (8, 6)

    for f in range(10, len(df), 10):
        print(f)
        # Slice the dataframe up until the current iteration
        df_slice = df.iloc[:f, :]

        # Pivot the data using the pivot_table method and sum the counts of duplicates
        pivot = df_slice.pivot_table(index='Z', columns='X', values='Count', aggfunc='sum')

        # Calculate the maximum value of the pivot table for the entire dataframe
        max_value = np.nanmax(pivot.values)

        # Create a new figure and plot the heatmap
        fig, ax = plt.subplots(figsize=fig_size)
        sns.heatmap(pivot, annot=False, cmap='YlOrRd', cbar=False, vmin=0, vmax=max_value, ax=ax)

        # Save the plot to a file in the tmpFiles directory
        save_at = os.path.join(script_dir, 'tmpFiles', str(i) + '.png')
        plt.savefig(save_at, dpi=300, bbox_inches='tight')

        # Close the figure to prevent memory issues
        plt.close(fig)

        # Increment the counter
        i += 1
try:
    ImageMaker()
except Exception as e:
    print(f"An error occurred while running imagemaker: {e}")
def VideoMaker():

    # Set the name and fps of the output video
    video_name = 'output.webm'
    fps = 5

    # Get a list of all the image filenames in the folder
    images = [img for img in os.listdir(folder_path) if img.endswith('.png')]

    # Sort the image filenames in ascending order
    images.sort(key=lambda x: int(x[:-4]))

    # Get the height and width of the first image in the folder
    frame = cv2.imread(os.path.join(folder_path, images[0]))
    height, width, layers = frame.shape

    # Create a VideoWriter object to write the frames to a video
    fourcc = cv2.VideoWriter_fourcc(*'V','P','8','0')
    video = cv2.VideoWriter(video_name, fourcc, fps, (width, height))

    # Iterate over each image and add it to the video
    for image in images:
        # Read the image from the file
        frame = cv2.imread(os.path.join(folder_path, image))
        frame = cv2.resize(frame, (width, height))
        video.write(frame)

    # Release the VideoWriter object
    video.release()
    for image in images:
        os.remove(os.path.join(folder_path, image))

VideoMaker()